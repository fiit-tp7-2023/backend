using Newtonsoft.Json;

namespace TAG.Models
{
    public class Player
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("name")]
        public required string Name { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("height")]
        public float Height { get; set; }
    }
}