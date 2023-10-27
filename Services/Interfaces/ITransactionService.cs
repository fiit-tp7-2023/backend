using TAG.DTOS;

namespace TAG.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionDTO?> GetTransactionAsync(string id);
        Task<IEnumerable<TransactionDTO>> SearchTransactionsAsync(TransactionSearchRequestDTO request);
    }
}
