using Newtonsoft.Json;

namespace TAG.Database.Relationships
{
    public class TaggedRelationship
    {
        [JsonProperty("value")]
        public int Value { get; set; } = 1;
    }
}
