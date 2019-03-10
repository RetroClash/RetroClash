using Newtonsoft.Json;

namespace RetroClash.Logic.Manager.Items
{
    public class Resource
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("vl")]
        public int Value { get; set; }
    }
}