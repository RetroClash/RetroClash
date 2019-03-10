using System;
using Newtonsoft.Json;

namespace RetroClash.Logic.Manager.Items
{
    public class Hero
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("building_id")]
        public int BuildingId { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("reg_time")]
        public DateTime RegenerationEndTime { get; set; }

        [JsonProperty("lvl")]
        public int Level { get; set; }

        [JsonIgnore]
        public int Health
        {
            get => RegenerationEndTime > DateTime.UtcNow
                ? (int) RegenerationEndTime.Subtract(DateTime.UtcNow).TotalSeconds
                : 0;
            set => RegenerationEndTime = DateTime.UtcNow.AddMinutes(value);
        }

        public void Upgrade()
        {
            Level++;
        }

        public void Toogle()
        {
            State = State <= 2 ? 3 : 2;
        }
    }
}