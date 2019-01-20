using Newtonsoft.Json;

namespace RetroClashCore.Logic.Manager.Items
{
    public class Resource
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("vl")]
        public int Value { get; set; }
    }
}