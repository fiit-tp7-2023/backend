using Newtonsoft.Json;

namespace TAG.Nodes
{
    public class NFTNode
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("uri")]
        public string? Uri { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }
    }
}
