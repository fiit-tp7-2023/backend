using Microsoft.IdentityModel.Tokens;
using Neo4jClient;
using Neo4jClient.Extensions;
using AutoMapper;
using TAG.Extensions;
using TAG.Constants;
using TAG.DTOS;
using TAG.Services.Interfaces;
using TAG.QueryResults;
using TAG.Database.Relationships;
using TAG.Database.Nodes;

namespace TAG.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IGraphClient _graphClient;
        private readonly IMapper _mapper;

        public TransactionService(IGraphClient graphClient, IMapper mapper)
        {
            _graphClient = graphClient;
            _mapper = mapper;
        }

        public async Task<TransactionDTO?> GetTransactionAsync(string id)
        {
            var queryResult = await _graphClient.Cypher
                .Match(
                    $"(sender:{NodeNames.ADDRESS})-[:{RelationshipNames.SENT}]->(transaction:{NodeNames.TRANSACTION})<-[:{RelationshipNames.RECEIVED}]-(receiver:{NodeNames.ADDRESS})",
                    $"(transaction)-[:{RelationshipNames.HAS_NFT}]->(nft:{NodeNames.NFT})"
                )
                .OptionalMatch($"(nft)-[rel:{RelationshipNames.TAGGED}]->(tag:{NodeNames.TAG})")
                .Where<TransactionNode>((transaction) => transaction.Id == id)
                .ReturnDistinct(
                    (sender, transaction, receiver, nft, rel, tag) =>
                        new TransactionQueryResult
                        {
                            Transaction = transaction.As<TransactionNode>(),
                            Sender = sender.As<AddressNode>(),
                            Receiver = receiver.As<AddressNode>(),
                            NFT = nft.As<NFTNode>(),
                            Tags = tag.CollectAsDistinct<TagNode>(),
                            TagRelations = rel.CollectAsDistinct<TaggedRelationship>()
                        }
                )
                .FirstOrDefaultAsync();

            return _mapper.Map<TransactionDTO>(queryResult);
        }

        public async Task<TransactionSearchResponseDTO> SearchTransactionsAsync(TransactionSearchRequestDTO request)
        {
            var query = _graphClient.Cypher
                .Match(
                    $"(sender:{NodeNames.ADDRESS})-[:{RelationshipNames.SENT}]->(transaction:{NodeNames.TRANSACTION})<-[:{RelationshipNames.RECEIVED}]-(receiver:{NodeNames.ADDRESS})",
                    $"(transaction)-[:{RelationshipNames.HAS_NFT}]->(nft:{NodeNames.NFT})"
                )
                .WhereAlwaysTrue()
                .AndWhereIf<AddressNode>(!request.SenderId.IsNullOrEmpty(), (sender) => sender.Id == request.SenderId)
                .AndWhereIf<AddressNode>(
                    !request.ReceiverId.IsNullOrEmpty(),
                    (receiver) => receiver.Id == request.ReceiverId
                )
                .AndWhereIf<NFTNode>(!request.NFTId.IsNullOrEmpty(), (nft) => nft.Id == request.NFTId)
                .OptionalMatchIf(
                    request.TagNames.IsNullOrEmpty(),
                    $"(nft)-[rel:{RelationshipNames.TAGGED}]->(tag:{NodeNames.TAG})"
                )
                .WhereIf<TagNode>(!request.TagNames.IsNullOrEmpty(), (tag) => tag.Type.In(request.TagNames));

            var count = await query.Return(transaction => transaction.CountDistinct()).FirstOrDefaultAsync();
            var queryResults = await query
                .ReturnDistinct(
                    (sender, transaction, receiver, nft, rel, tag) =>
                        new TransactionQueryResult
                        {
                            Transaction = transaction.As<TransactionNode>(),
                            Sender = sender.As<AddressNode>(),
                            Receiver = receiver.As<AddressNode>(),
                            NFT = nft.As<NFTNode>(),
                            Tags = tag.CollectAsDistinct<TagNode>(),
                            TagRelations = rel.CollectAsDistinct<TaggedRelationship>()
                        }
                )
                .OrderByNodeId("sender")
                .Paginate(request.PageNumber, request.PageSize)
                .ResultsAsync;

            return new TransactionSearchResponseDTO
            {
                PageCount = (int)Math.Ceiling((double)count / request.PageSize),
                Transactions = _mapper.Map<IEnumerable<TransactionDTO>>(queryResults)
            };
            ;
        }
    }
}
