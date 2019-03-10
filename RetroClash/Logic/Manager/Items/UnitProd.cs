using Newtonsoft.Json;

namespace RetroClash.Logic.Manager.Items
{
    public class UnitProd
    {
        [JsonProperty("unit_type", DefaultValueHandling = DefaultValueHandling.Include)]
        public int UnitType { get; set; }
    }
}