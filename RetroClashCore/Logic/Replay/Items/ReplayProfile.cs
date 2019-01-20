using System.Collections.Generic;
using Newtonsoft.Json;

namespace RetroClashCore.Logic.Replay.Items
{
    public class ReplayProfile
    {
        [JsonProperty("alliance_units")] public List<ReplayUnitItem> AllianceUnits = new List<ReplayUnitItem>();

        [JsonProperty("hero_health")] public List<ReplayUnitItem> HeroHealth = new List<ReplayUnitItem>();

        [JsonProperty("hero_states")] public List<ReplayUnitItem> HeroStates = new List<ReplayUnitItem>();

        [JsonProperty("hero_upgrade")] public List<ReplayUnitItem> HeroUpgrade = new List<ReplayUnitItem>();

        [JsonProperty("resources")] public List<ReplayUnitItem> Resources = new List<ReplayUnitItem>();

        [JsonProperty("spells")] public List<ReplayUnitItem> Spells = new List<ReplayUnitItem>();

        [JsonProperty("spell_upgrades")] public List<ReplayUnitItem> SpellUpgrades = new List<ReplayUnitItem>();

        [JsonProperty("units")] public List<ReplayUnitItem> Units = new List<ReplayUnitItem>();

        [JsonProperty("unit_upgrades")] public List<ReplayUnitItem> UnitUpgrades = new List<ReplayUnitItem>();

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alliance_name")]
        public string AllianceName { get; set; }

        [JsonProperty("badge_id")]
        public int BadgeId { get; set; }

        [JsonProperty("league_type")]
        public int League { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("town_hall_lvl")]
        public int TownHallLevel { get; set; }

        [JsonProperty("castle_lvl")]
        public int CastleLevel { get; set; }

        [JsonProperty("castle_total")]
        public int CastleTotal { get; set; }

        [JsonProperty("castle_used")]
        public int CastleUsed { get; set; }
    }
}