using Newtonsoft.Json;

namespace TAG.Database.Nodes
{
    public class TagNode
    {
        [JsonProperty("type")]
        public string Type { get; set; } = null!;
    }
}
