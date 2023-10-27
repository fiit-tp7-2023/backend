using Neo4jClient;
using TAG.Nodes;
using TAG.Extensions;
using TAG.Constants;
using TAG.DTOS;
using TAG.Services.Interfaces;

namespace TAG.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IGraphClient _graphClient;

        public TransactionService(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public async Task<TransactionDTO?> GetTransactionAsync(string id)
        {
            var result = await _graphClient.Cypher
                .Match(
                    $"(sender:{NodeNames.ADDRESS})-[:{RelationshipNames.SENT}]->(transaction:{NodeNames.TRANSACTION})<-[:{RelationshipNames.RECEIVED}]-(receiver:{NodeNames.ADDRESS})",
                    $"(transaction)-[:{RelationshipNames.HAS_NFT}]->(nft:{NodeNames.NFT})"
                )
                .Where<TransactionNode>((transaction) => transaction.Id == id)
                .Return(
                    (sender, transaction, receiver, nft) =>
                        new TransactionDTO
                        {
                            Id = transaction.As<TransactionNode>().Id,
                            Amount = transaction.As<TransactionNode>().Amount,
                            Sender = sender.As<AddressNode>(),
                            Receiver = receiver.As<AddressNode>(),
                            NFT = nft.As<NFTNode>()
                        }
                )
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<TransactionDTO>> SearchTransactionsAsync(TransactionSearchRequestDTO request)
        {
            var result = await _graphClient.Cypher
                .Match(
                    $"(sender:{NodeNames.ADDRESS})-[:{RelationshipNames.SENT}]->(transaction:{NodeNames.TRANSACTION})<-[:{RelationshipNames.RECEIVED}]-(receiver:{NodeNames.ADDRESS})",
                    $"(transaction)-[:{RelationshipNames.HAS_NFT}]->(nft:{NodeNames.NFT})"
                )
                .WhereIf<AddressNode>(request.SenderId != null, (sender) => sender.Id == request.SenderId)
                .WhereIf<AddressNode>(request.ReceiverId != null, (receiver) => receiver.Id == request.ReceiverId)
                .WhereIf<NFTNode>(request.NFTId != null, (nft) => nft.Id == request.NFTId)
                .Return(
                    (sender, transaction, receiver, nft) =>
                        new TransactionDTO
                        {
                            Id = transaction.As<TransactionNode>().Id,
                            Amount = transaction.As<TransactionNode>().Amount,
                            Sender = sender.As<AddressNode>(),
                            Receiver = receiver.As<AddressNode>(),
                            NFT = nft.As<NFTNode>()
                        }
                )
                .OrderByNodeId("sender")
                .Paginate(request.PageNumber, request.PageSize)
                .ResultsAsync;

            return result;
        }
    }
}
