using Newtonsoft.Json;

namespace TAG.Nodes
{
    public class Attribute
    {
        [JsonProperty("trait_type")]
        public string TraitType { get; set; } = null!;

        [JsonProperty("value")]
        public string Value { get; set; } = null!;
    }
}
