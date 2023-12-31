using Newtonsoft.Json;

namespace TAG.Database.Nodes
{
    public class TransactionNode
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}
