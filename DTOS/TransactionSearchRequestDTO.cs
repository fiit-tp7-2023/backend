namespace TAG.DTOS
{
    public class TransactionSearchRequestDTO : PaginationDTO
    {
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string? NFTId { get; set; }
    }
}
