using TAG.Nodes;

namespace TAG.DTOS
{
    public class NFTDTO : NFTNode
    {
        public IEnumerable<TagRelationDTO> Tags { get; set; } = null!;
    }
}
