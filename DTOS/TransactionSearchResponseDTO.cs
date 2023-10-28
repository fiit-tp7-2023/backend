namespace TAG.DTOS
{
    public class TransactionSearchResponseDTO : PaginationResponseDTO
    {
        public IEnumerable<TransactionDTO> Transactions { get; set; } = null!;
    }
}
