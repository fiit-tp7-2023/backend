using Newtonsoft.Json;

namespace TAG.Entities
{
    public class Address
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
    }
}
