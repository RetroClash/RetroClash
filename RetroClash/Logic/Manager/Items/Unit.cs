using Newtonsoft.Json;

namespace RetroClash.Logic.Manager.Items
{
    public class Unit
    {
        [JsonProperty("cnt")]
        public int Count { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("lvl")]
        public int Level { get; set; }
    }
}