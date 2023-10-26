using TAG.Models;

namespace TAG.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionModel?> GetTransactionAsync(string id);
        Task<IEnumerable<TransactionModel>> SearchTransactionsAsync(TransactionSearchRequest request);
    }
}
