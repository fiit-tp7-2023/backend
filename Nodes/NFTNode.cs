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

        [JsonIgnore]
        [JsonProperty("attributes")]
        public string? AttributesString { get; set; }

        public IEnumerable<Attribute> Attributes
        {
            get
            {
                if (AttributesString == null)
                {
                    return new List<Attribute>();
                }

                return JsonConvert.DeserializeObject<List<Attribute>>(AttributesString) ?? new List<Attribute>();
            }
        }
    }
}
