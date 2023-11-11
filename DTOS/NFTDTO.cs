using TAG.Nodes;

namespace TAG.DTOS
{
    public class NFTDTO : NFTNode
    {
        public IEnumerable<string> Tags { get; set; } = null!;
    }
}
