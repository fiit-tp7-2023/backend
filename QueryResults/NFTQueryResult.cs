using TAG.Nodes;

namespace TAG.QueryResults
{
    public class NFTQueryResult
    {
        public NFTNode NFT { get; set; } = null!;
        public IEnumerable<TagNode> Tags { get; set; } = null!;
        public IEnumerable<TagRelationNode> TagRelations { get; set; } = null!;
    }
}
