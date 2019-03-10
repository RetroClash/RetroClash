using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClash.Core.Database;
using RetroClash.Logic.Battle;
using RetroClash.Logic.Manager;
using RetroClash.Logic.Replay.Items;
using RetroClash.Logic.StreamEntry;
using RetroGames.Helpers;

namespace RetroClash.Logic
{
    public class Player : IDisposable
    {
        [JsonProperty("achievements")] public Achievements Achievements = new Achievements();

        [JsonProperty("heroes")] public LogicHeroManager HeroManager = new LogicHeroManager();

        [JsonIgnore] public LogicGameObjectManager LogicGameObjectManager = new LogicGameObjectManager();

        [JsonProperty("resources")] public LogicResourcesManager ResourcesManager = new LogicResourcesManager();

        [JsonProperty("shield")] public LogicShield Shield = new LogicShield();

        [JsonProperty("stream")] public List<AvatarStreamEntry> Stream = new List<AvatarStreamEntry>(20);

        [JsonProperty("units")] public Units Units = new Units();

        public Player(long id, string token)
        {
            AccountId = id;

            Name = "RetroClash";
            PassToken = token;
            ExpLevel = 1;
            TutorialSteps = 10;
            Language = "EN";
            Diamonds = 1000000000;

            ResourcesManager.Initialize();
            LogicGameObjectManager.Json = Resources.Levels.StartingHome;
        }

        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("alliance_id")]
        public long AllianceId { get; set; }

        [JsonProperty("fb_id")]
        public string FacebookId { get; set; }

        [JsonProperty("account_name")]
        public string Name { get; set; }

        [JsonProperty("device_name")]
        public string DeviceName { get; set; }

        [JsonProperty("pass_token")]
        public string PassToken { get; set; }

        [JsonProperty("exp_level")]
        public int ExpLevel { get; set; }

        [JsonProperty("exp_points")]
        public int ExpPoints { get; set; }

        [JsonProperty("tutorial_steps")]
        public int TutorialSteps { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonIgnore]
        public Device Device { get; set; }

        [JsonIgnore]
        public PvbBattle Battle { get; set; }

        [JsonProperty("diamonds")]
        public int Diamonds { get; set; }

        public void Dispose()
        {
            Device = null;
            Units = null;
            Battle = null;
            Stream = null;
            HeroManager = null;
            Achievements = null;
            ResourcesManager = null;
            LogicGameObjectManager = null;
        }

        public void AddEntry(AvatarStreamEntry entry)
        {
            while (Stream.Count >= 20)
                Stream.RemoveAt(0);

            entry.Id = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            Stream.Add(entry);
        }

        public async Task LogicClientHome(MemoryStream stream)
        {
            await stream.WriteInt(0);

            await stream.WriteLong(AccountId); // Account Id

            await stream.WriteString(LogicGameObjectManager.Json);

            await stream.WriteInt(Shield.ShieldSecondsLeft);
            await stream.WriteInt(0);
            await stream.WriteInt(0);
        }

        public async Task LogicClientAvatar(MemoryStream stream)
        {
            await stream.WriteInt(0);
            await stream.WriteLong(AccountId); // Account Id
            await stream.WriteLong(AccountId); // Home Id

            if (AllianceId > 0)
            {
                var alliance = await Resources.AllianceCache.GetAlliance(AllianceId);

                if (alliance != null)
                {
                    stream.WriteBool(true);
                    await stream.WriteLong(AllianceId); // Alliance Id
                    await stream.WriteString(alliance.Name); // Alliance Name
                    await stream.WriteInt(alliance.Badge); // Alliance Badge
                    await stream.WriteInt(alliance.Members.Find(x => x.AccountId == AccountId)
                        .Role); // Alliance Role

                    stream.WriteByte(1);
                    await stream.WriteLong(AllianceId); // Alliance Id

                    stream.WriteByte(1);
                    await stream.WriteLong(AllianceId); // Alliance Id
                }
                else
                {
                    stream.WriteBool(false);
                }
            }
            else
            {
                stream.WriteBool(false);
            }

            await stream.WriteInt(LogicUtils.GetLeagueByScore(Score)); // League Type

            await stream.WriteInt(0); // Alliance Castle Level
            await stream.WriteInt(10); // Alliance Total Capacity
            await stream.WriteInt(0); // Alliance Used Capacity
            await stream.WriteInt(LogicGameObjectManager?.GetTownHallLevel() ?? 1); // Townhall Level

            await stream.WriteString(Name); // Name
            await stream.WriteString(null); // Facebook Id 

            await stream.WriteInt(ExpLevel); // Exp Level
            await stream.WriteInt(ExpPoints); // Exp Points

            await stream.WriteInt(Diamonds); // Diamonts
            await stream.WriteInt(0); // Current Diamonts
            await stream.WriteInt(0); // Free Diamonts

            await stream.WriteInt(0); // Unknown

            await stream.WriteInt(Score); // Score

            await stream.WriteInt(0); // Attack Win Count
            await stream.WriteInt(0); // Attack Lose Count

            await stream.WriteInt(0); // Defense Win Count
            await stream.WriteInt(0); // Defense Lose Count

            stream.WriteBool(false); // Name Set By User 
            await stream.WriteInt(0);

            // Resource Caps
            await stream.WriteInt(0);

            // Resources
            await stream.WriteInt(ResourcesManager.Count);
            foreach (var resource in ResourcesManager)
            {
                await stream.WriteInt(resource.Id);
                await stream.WriteInt(resource.Value);
            }

            // Troops
            await stream.WriteInt(Units.Troops.Count);
            foreach (var troop in Units.Troops)
            {
                await stream.WriteInt(troop.Id);
                await stream.WriteInt(troop.Count);
            }

            // Spells
            await stream.WriteInt(Units.Spells.Count);
            foreach (var spell in Units.Spells)
            {
                await stream.WriteInt(spell.Id);
                await stream.WriteInt(spell.Count);
            }

            // Troop Levels
            await stream.WriteInt(Units.Troops.Count);
            foreach (var troop in Units.Troops)
            {
                await stream.WriteInt(troop.Id);
                await stream.WriteInt(troop.Level);
            }

            // Spell Levels
            await stream.WriteInt(Units.Spells.Count);
            foreach (var spell in Units.Spells)
            {
                await stream.WriteInt(spell.Id);
                await stream.WriteInt(spell.Level);
            }

            // Hero Levels
            await stream.WriteInt(HeroManager.Count);
            foreach (var hero in HeroManager)
            {
                await stream.WriteInt(hero.Id);
                await stream.WriteInt(hero.Level);
            }

            // Hero Health
            await stream.WriteInt(HeroManager.Count);
            foreach (var hero in HeroManager)
            {
                await stream.WriteInt(hero.Id);
                await stream.WriteInt(hero.Health);
            }

            // Hero State
            await stream.WriteInt(HeroManager.Count);
            foreach (var hero in HeroManager)
            {
                await stream.WriteInt(hero.Id);
                await stream.WriteInt(hero.State);
            }

            // Alliance Units
            await stream.WriteInt(0);

            // Tutorials
            await stream.WriteInt(TutorialSteps);
            for (var index = 21000000; index < 21000000 + TutorialSteps; index++)
                await stream.WriteInt(index);

            // Achievements
            await stream.WriteInt(Achievements.Count);
            foreach (var achievement in Achievements)
                await stream.WriteInt(achievement.Id);

            // Achievement Progress
            await stream.WriteInt(Achievements.Count);
            foreach (var achievement in Achievements)
            {
                await stream.WriteInt(achievement.Id);
                await stream.WriteInt(achievement.Data);
            }

            // Npc Map Progress
            await stream.WriteInt(50);
            for (var index = 17000000; index < 17000050; index++)
            {
                await stream.WriteInt(index);
                await stream.WriteInt(3);
            }

            await stream.WriteInt(0); // Npc Looted Gold DataSlot
            await stream.WriteInt(0); // Npc Looted Elixir DataSlot
        }

        public async Task AvatarRankingEntry(MemoryStream stream)
        {
            await stream.WriteInt(ExpLevel); // Exp Level
            await stream.WriteInt(0); // Attack Win Count
            await stream.WriteInt(0); // Attack Lose Count
            await stream.WriteInt(0); // Defense Win Count
            await stream.WriteInt(0); // Defense Lose Count
            await stream.WriteInt(LogicUtils.GetLeagueByScore(Score)); // League Type

            await stream.WriteString(Language); // Country
            await stream.WriteLong(AccountId); // Home Id

            if (AllianceId > 0)
            {
                var alliance = await Resources.AllianceCache.GetAlliance(AllianceId);

                if (alliance != null)
                {
                    stream.WriteBool(true);
                    await stream.WriteLong(AllianceId); // Clan Id
                    await stream.WriteString(alliance.Name); // Clan Name
                    await stream.WriteInt(alliance.Badge); // Badge
                }
                else
                {
                    AllianceId = 0;
                    stream.WriteBool(false);
                }
            }
            else
            {
                stream.WriteBool(false);
            }
        }

        public void AddDiamonds(int value)
        {
            Diamonds += value;
        }

        public bool UseDiamonds(int value)
        {
            if (Diamonds < value)
                return false;

            Diamonds -= value;

            return true;
        }

        public async Task Update()
        {
            await Redis.CachePlayer(this);
        }

        public ReplayProfile GetReplayProfile(bool attacker)
        {
            var profile = new ReplayProfile
            {
                Name = Name,
                Score = Score,
                League = LogicUtils.GetLeagueByScore(Score),
                TownHallLevel = LogicGameObjectManager.GetTownHallLevel(),
                CastleLevel = 1,
                CastleTotal = 15,
                CastleUsed = 0,
                BadgeId = 13000000,
                AllianceName = "RetroClash"
            };

            foreach (var troop in Units.Troops)
                if (troop.Count > 0)
                    profile.Units.Add(new ReplayUnitItem
                    {
                        Id = troop.Id,
                        Cnt = troop.Count
                    });

            foreach (var spell in Units.Spells)
                if (spell.Count > 0)
                    profile.Spells.Add(new ReplayUnitItem
                    {
                        Id = spell.Id,
                        Cnt = spell.Count
                    });

            foreach (var unit in Units.Troops)
                profile.UnitUpgrades.Add(new ReplayUnitItem
                {
                    Id = unit.Id,
                    Cnt = unit.Level
                });

            foreach (var spell in Units.Spells)
                profile.SpellUpgrades.Add(new ReplayUnitItem
                {
                    Id = spell.Id,
                    Cnt = spell.Level
                });

            if (attacker)
                foreach (var resource in ResourcesManager)
                    profile.Resources.Add(new ReplayUnitItem
                    {
                        Id = resource.Id,
                        Cnt = resource.Value
                    });

            foreach (var hero in HeroManager)
                profile.HeroStates.Add(new ReplayUnitItem
                {
                    Id = hero.Id,
                    Cnt = hero.State
                });

            foreach (var hero in HeroManager)
                profile.HeroHealth.Add(new ReplayUnitItem
                {
                    Id = hero.Id,
                    Cnt = hero.Health
                });

            foreach (var hero in HeroManager)
                profile.HeroUpgrade.Add(new ReplayUnitItem
                {
                    Id = hero.Id,
                    Cnt = hero.Level
                });

            return profile;
        }
    }
}