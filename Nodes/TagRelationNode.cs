using Newtonsoft.Json;

namespace TAG.Nodes
{
    public class TagRelationNode
    {
        [JsonProperty("value")]
        public int Value { get; set; } = 1;
    }
}
