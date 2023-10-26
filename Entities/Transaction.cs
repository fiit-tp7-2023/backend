using Newtonsoft.Json;

namespace TAG.Entities
{
    public class Transaction
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}
