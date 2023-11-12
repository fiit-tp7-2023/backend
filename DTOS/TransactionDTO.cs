using TAG.Database.Nodes;

namespace TAG.DTOS
{
    public class TransactionDTO : TransactionNode
    {
        public AddressNode Sender { get; set; } = null!;
        public AddressNode Receiver { get; set; } = null!;
        public NFTDTO NFT { get; set; } = null!;
    }
}
