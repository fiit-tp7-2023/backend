namespace TAG.DTOS
{
    public class TransactionSearchRequestDTO : PaginationRequestDTO
    {
        public string? SenderAddress { get; set; }
        public string? ReceiverAddress { get; set; }
        public string? NFTAddress { get; set; }
        public IEnumerable<string>? TagNames { get; set; }
    }
}
