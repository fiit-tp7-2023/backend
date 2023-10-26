namespace TAG.Models
{
    public class TransactionSearchRequest : Pagination
    {
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string? NFTId { get; set; }
    }
}
