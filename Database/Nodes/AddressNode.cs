using Newtonsoft.Json;

namespace TAG.Database.Nodes
{
    public class AddressNode
    {
        [JsonProperty("address")]
        public string Address { get; set; } = null!;

        [JsonProperty("createdAtBlock")]
        public ulong CreatedAtBlock { get; set; }
    }
}
