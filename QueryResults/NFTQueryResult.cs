using TAG.Database.Nodes;
using TAG.Database.Relationships;

namespace TAG.QueryResults
{
    public class NFTQueryResult
    {
        public NFTNode NFT { get; set; } = null!;
        public IEnumerable<TagNode> Tags { get; set; } = null!;
        public IEnumerable<TaggedRelationship> TagRelations { get; set; } = null!;
    }
}
