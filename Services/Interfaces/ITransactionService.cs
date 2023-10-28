using TAG.DTOS;

namespace TAG.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionDTO?> GetTransactionAsync(string id);
        Task<TransactionSearchResponseDTO> SearchTransactionsAsync(TransactionSearchRequestDTO request);
    }
}
