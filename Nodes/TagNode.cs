using Newtonsoft.Json;

namespace TAG.Nodes
{
    public class TagNode
    {
        [JsonProperty("type")]
        public string Type { get; set; } = null!;
    }
}
