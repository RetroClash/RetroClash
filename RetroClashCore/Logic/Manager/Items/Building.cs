using System;
using Newtonsoft.Json;

namespace RetroClashCore.Logic.Manager.Items
{
    public class Building
    {
        [JsonProperty("attack_mode")]
        public bool AttackMode { get; set; }

        [JsonProperty("ammo")]
        public int Ammo { get; set; }

        [JsonProperty("data")]
        public int Data { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("lvl", DefaultValueHandling = DefaultValueHandling.Include)]
        public int Level { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("res_time")]
        public int ResetTime { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        [JsonProperty("boost_t")]
        public int BoostTime
        {
            get => BoostEndTime > DateTime.UtcNow ? (int) BoostEndTime.Subtract(DateTime.UtcNow).TotalSeconds : 0;
            set => BoostEndTime = DateTime.UtcNow.AddMinutes(value);
        }

        [JsonProperty("const_t")]
        public int RemainingConstructionSeconds
        {
            get => ConstructionFinish > DateTime.UtcNow
                ? (int) ConstructionFinish.Subtract(DateTime.UtcNow).TotalSeconds
                : 0;
            set => ConstructionFinish = DateTime.UtcNow.AddSeconds(value);
        }

        [JsonProperty("unit_prod")]
        public UnitProd UnitProd { get; set; }

        [JsonProperty("storage_type")]
        public int StorageType { get; set; }

        [JsonProperty("boost_end")]
        public DateTime BoostEndTime { get; set; }

        [JsonProperty("construction_finish")]
        public DateTime ConstructionFinish { get; set; }
    }
}