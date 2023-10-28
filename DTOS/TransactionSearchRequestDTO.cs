namespace TAG.DTOS
{
    public class TransactionSearchRequestDTO : PaginationRequestDTO
    {
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string? NFTId { get; set; }
    }
}
