using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroClash.Core.Database;
using RetroClash.Logic.Replay;
using RetroClash.Logic.Replay.Items;
using RetroClash.Logic.StreamEntry.Avatar;
using RetroGames.Helpers;

namespace RetroClash.Logic.Battle
{
    public class PvbBattle
    {
        public LogicReplay Replay = new LogicReplay();

        public PvbBattle(Player attacker)
        {
            Attacker = attacker;
            Replay.Attacker = Attacker.GetReplayProfile(true);
            Replay.Level = Attacker.LogicGameObjectManager;
        }

        public Player Attacker { get; set; }

        public Player Defender { get; set; }

        public string GetReplayJson => JsonConvert.SerializeObject(Replay, new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.None
        });

        public void SetDefender(Player defender)
        {
            Defender = defender;
            Replay.Defender = defender.GetReplayProfile(false);
            Replay.Level = defender.LogicGameObjectManager;
            Replay.Timestamp = Utils.GetCurrentTimestamp;
        }

        public void RecordCommand(ReplayCommand cmd)
        {
            if (!Replay.Commands.Contains(cmd) && Replay.Commands.Count < 500)
                Replay.Commands.Add(cmd);
        }

        public async Task EndBattle()
        {
            if (Replay.Commands.Count <= 0) return;

            var random = new Random();

            var originalScore = Attacker.Score;
            var attackerReward = random.Next(10, 25);

            Attacker.Score += attackerReward;

            var id = await ReplayDb.Save(GetReplayJson);

            if (id > 0)
                Attacker.AddEntry(new BattleReportStreamEntry
                {
                    MajorVersion = Resources.Fingerprint.GetMajorVersion,
                    Build = Resources.Fingerprint.GetBuildVersion,
                    ContentVersion = Resources.Fingerprint.GetContentVersion,
                    CreationDateTime = DateTime.UtcNow,
                    IsRevengeUsed = true, // Revenge is not supported atm
                    SenderAvatarId = Defender.AccountId,
                    SenderName = Defender.Name,
                    SenderLevel = Defender.ExpLevel,
                    SenderLeagueType = LogicUtils.GetLeagueByScore(Defender.Score),
                    ShardId = 0,
                    ReplayId = id,
                    BattleLogJson = JsonConvert.SerializeObject(new BattleLog
                    {
                        // Here we use random values
                        Loot = new[]
                        {
                            new[] {3000001, random.Next(1000, 100000)}, new[] {3000002, random.Next(1000, 100000)}
                        },
                        Units = new[]
                        {
                            new[] {4000000, random.Next(10, 50)}, new[] {4000001, random.Next(10, 50)},
                            new[] {4000002, random.Next(10, 50)}, new[] {4000003, random.Next(10, 50)},
                            new[] {4000004, random.Next(10, 50)}, new[] {4000005, random.Next(10, 50)},
                            new[] {4000006, random.Next(10, 50)}, new[] {4000007, random.Next(10, 50)},
                            new[] {4000008, random.Next(10, 50)}, new[] {4000009, random.Next(10, 50)}
                        },

                        Levels = new int[0][],
                        Spells = new int[0][],
                        Stats = new BattleLogStats
                        {
                            TownHallDestroyed = true,
                            DestructionPercentage = random.Next(0, 100),
                            AllianceName = "RetroClash",
                            AllianceUsed = false,
                            AttackerScore = attackerReward,
                            BattleEnded = true,
                            BattleTime = Replay.EndTick,
                            DefenderScore = random.Next(-30, -15),
                            HomeId = new[] {0, 1},
                            OriginalScore = originalScore
                        }
                    })
                });
        }

        public void Dispose()
        {
            Replay = null;
            Defender = null;
        }
    }
}