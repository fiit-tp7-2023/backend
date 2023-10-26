using TAG.Entities;

namespace TAG.Models
{
    public class TransactionModel : Transaction
    {
        public Address Sender { get; set; } = null!;
        public Address Receiver { get; set; } = null!;
        public NFT NFT { get; set; } = null!;
    }
}
