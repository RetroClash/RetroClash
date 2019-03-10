using Newtonsoft.Json;

namespace RetroClash.Logic.Manager.Items
{
    public class Obstacle
    {
        [JsonProperty("data")]
        public int Data { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("lmv")]
        public int Lmv { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }
    }
}