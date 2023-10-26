using Neo4jClient;
using TAG.Entities;
using TAG.Extensions;
using TAG.Constants;
using TAG.Models;
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

        public async Task<TransactionModel?> GetTransactionAsync(string id)
        {
            var result = await _graphClient.Cypher
                .Match(
                    $"(sender:{EntityNames.ADDRESS})-[:{RelationshipNames.SENT}]->(transaction:{EntityNames.TRANSACTION})<-[:{RelationshipNames.RECEIVED}]-(receiver:{EntityNames.ADDRESS})",
                    $"(transaction)-[:{RelationshipNames.HAS_NFT}]->(nft:{EntityNames.NFT})"
                )
                .Where<Transaction>((transaction) => transaction.Id == id)
                .Return(
                    (sender, transaction, receiver, nft) =>
                        new TransactionModel
                        {
                            Id = transaction.As<Transaction>().Id,
                            Amount = transaction.As<Transaction>().Amount,
                            Sender = sender.As<Address>(),
                            Receiver = receiver.As<Address>(),
                            NFT = nft.As<NFT>()
                        }
                )
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<TransactionModel>> SearchTransactionsAsync(TransactionSearchRequest request)
        {
            var result = await _graphClient.Cypher
                .Match(
                    $"(sender:{EntityNames.ADDRESS})-[:{RelationshipNames.SENT}]->(transaction:{EntityNames.TRANSACTION})<-[:{RelationshipNames.RECEIVED}]-(receiver:{EntityNames.ADDRESS})",
                    $"(transaction)-[:{RelationshipNames.HAS_NFT}]->(nft:{EntityNames.NFT})"
                )
                .WhereIf<Address>(request.SenderId != null, (sender) => sender.Id == request.SenderId)
                .WhereIf<Address>(request.ReceiverId != null, (receiver) => receiver.Id == request.ReceiverId)
                .WhereIf<NFT>(request.NFTId != null, (nft) => nft.Id == request.NFTId)
                .Return(
                    (sender, transaction, receiver, nft) =>
                        new TransactionModel
                        {
                            Id = transaction.As<Transaction>().Id,
                            Amount = transaction.As<Transaction>().Amount,
                            Sender = sender.As<Address>(),
                            Receiver = receiver.As<Address>(),
                            NFT = nft.As<NFT>()
                        }
                )
                .OrderByNodeId("sender")
                .Paginate(request.PageNumber, request.PageSize)
                .ResultsAsync;

            return result;
        }
    }
}
