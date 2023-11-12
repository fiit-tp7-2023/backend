using Newtonsoft.Json;

namespace TAG.Database.Nodes
{
    public class AddressNode
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
    }
}
