using Newtonsoft.Json;

namespace TAG.Nodes
{
    public class AddressNode
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
    }
}
