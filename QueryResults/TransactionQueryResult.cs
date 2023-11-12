using TAG.Nodes;

namespace TAG.QueryResults
{
    public class TransactionQueryResult
    {
        public TransactionNode Transaction { get; set; } = null!;
        public AddressNode Sender { get; set; } = null!;
        public AddressNode Receiver { get; set; } = null!;
        public NFTNode NFT { get; set; } = null!;
        public IEnumerable<TagNode> Tags { get; set; } = null!;
        public IEnumerable<TagRelationNode> TagRelations { get; set; } = null!;
    }
}
