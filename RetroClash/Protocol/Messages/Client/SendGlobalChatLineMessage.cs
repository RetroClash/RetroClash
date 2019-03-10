using System;
using System.Threading.Tasks;
using RetroClash.Core.Database;
using RetroClash.Logic;
using RetroClash.Logic.Manager.Items;
using RetroClash.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Client
{
    public class SendGlobalChatLineMessage : PiranhaMessage
    {
        public SendGlobalChatLineMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public string Message { get; set; }

        public override void Decode()
        {
            Message = Reader.ReadString();
        }

        public override async Task Process()
        {
            if (Message.StartsWith("/"))
                switch (Message.Split(' ')[0])
                {
                    case "/help":
                    {
                        await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                        {
                            Message =
                                "Available commands:\n\n/stats\n  -> View the server stats.\n/clear [obstacles/traps/decorations]\n  -> Clear all obstacles.\n/help\n  -> List of all commands.\n/rename\n  -> Change your name again.\n/replay\n  -> Watch a random replay.\n/prebase [level]\n  -> Load a premade base from level 1 to 6.\n/reset\n  -> Reset your village and start from beginning.\n/wall [level]\n  -> Set the level of all walls.",
                            Name = "DebugManager",
                            ExpLevel = 100,
                            League = 16
                        });
                        break;
                    }

                    case "/prebase":
                    {
                        var lvl = Convert.ToInt32(Message.Split(' ')[1]);
                        if (lvl > 0 && lvl < 7)
                        {
                            Device.Player.LogicGameObjectManager.Json = Resources.Levels.Prebases[lvl - 1];
                            Device.Player.HeroManager.Clear();
                            Device.Player.Units.Troops.Clear();
                            Device.Player.Units.Spells.Clear();

                            await Resources.Gateway.Send(new OwnHomeDataMessage(Device));

                            await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                            {
                                Message = $"Base {lvl} has been set.",
                                Name = "DebugManager",
                                ExpLevel = 100,
                                League = 16
                            });
                        }
                        break;
                    }

                    case "/reset":
                    {
                        Device.Player.LogicGameObjectManager.Json = Resources.Levels.StartingHome;
                        Device.Player.Achievements.Clear();
                        Device.Player.HeroManager.Clear();
                        Device.Player.Shield.RemoveShield();
                        Device.Player.Units.Spells.Clear();
                        Device.Player.Units.Troops.Clear();

                        await Resources.Gateway.Send(new OwnHomeDataMessage(Device));

                        await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                        {
                            Message = "Village has been set to default.",
                            Name = "DebugManager",
                            ExpLevel = 100,
                            League = 16
                        });

                        break;
                    }

                    case "/rename":
                    {
                        Device.Player.TutorialSteps = 10;
                        Device.Player.ExpLevel = 1;

                        await Resources.Gateway.Send(new OwnHomeDataMessage(Device));

                        break;
                    }

                    /*case "/add":
                    {
                        var type = Message.Split(' ')[1];

                        switch (type)
                        {
                            case "trophies":
                            {
                                Device.Player.Score += Convert.ToInt32(Message.Split(' ')[2]);
                                break;
                            }

                            default:
                            {
                                await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                                {
                                    Message = "Invalid type.",
                                    Name = "DebugManager",
                                    ExpLevel = 100,
                                    League = 16
                                });
                                break;
                            }
                        }

                        await Resources.Gateway.Send(new OwnHomeDataMessage(Device));

                        break;
                    }*/

                    case "/remove":
                    {
                        var type = Message.Split(' ')[1];

                        switch (type)
                        {
                            case "trophies":
                            {
                                Device.Player.Score -= Convert.ToInt32(Message.Split(' ')[2]);
                                break;
                            }

                            default:
                            {
                                await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                                {
                                    Message = "Invalid type.",
                                    Name = "DebugManager",
                                    ExpLevel = 100,
                                    League = 16
                                });
                                break;
                            }
                        }

                        await Resources.Gateway.Send(new OwnHomeDataMessage(Device));

                        break;
                    }

                    case "/clear":
                    {
                        var type = Message.Split(' ')[1];

                        switch (type)
                        {
                            case "obstacles":
                            {
                                Device.Player.LogicGameObjectManager.Obstacles.Clear();
                                break;
                            }

                            case "traps":
                            {
                                Device.Player.LogicGameObjectManager.Traps.Clear();
                                break;
                            }

                            case "decorations":
                            {
                                Device.Player.LogicGameObjectManager.Decorations.Clear();                            
                                break;
                            }

                            default:
                            {
                                await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                                {
                                    Message = "Invalid type.",
                                    Name = "DebugManager",
                                    ExpLevel = 100,
                                    League = 16
                                });
                                break;
                            }
                        }

                        await Resources.Gateway.Send(new OwnHomeDataMessage(Device));

                        break;
                    }

                    case "/replay":
                    {
                        var replay = await ReplayDb.GetRandom();

                        if (replay != null)
                            await Resources.Gateway.Send(new HomeBattleReplayDataMessage(Device)
                            {
                                Replay = replay
                            });
                        else
                            await Resources.Gateway.Send(new HomeBattleReplayFailedMessage(Device));

                        break;
                    }

                    case "/stats":
                    {
                        var uptime = DateTime.UtcNow - Resources.StartDateTime;

                        await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                        {
                            Message =
                                $"Players online: {Resources.PlayerCache.Count}\nPlayers cached: {Redis.CachedPlayers()}\nUsed RAM: {GC.GetTotalMemory(false) / 1024 / 1024}MB\nUptime: {uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s\nServer version: {Configuration.Version}\nFingerprint version: {Resources.Fingerprint.GetMajorVersion}.{Resources.Fingerprint.GetBuildVersion}.{Resources.Fingerprint.GetContentVersion}",
                            Name = "DebugManager",
                            ExpLevel = 100,
                            League = 16
                        });

                        break;
                    }

                    case "/wall":
                    {
                        var lvl = Convert.ToInt32(Message.Split(' ')[1]);
                        if (lvl > 0 && lvl < 12)
                        {
                            foreach (var building in Device.Player.LogicGameObjectManager.Buildings)
                            {
                                if (building.Data != 1000010) continue;
                                building.Level = lvl - 1;
                            }

                            await Resources.Gateway.Send(new OwnHomeDataMessage(Device));

                            await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                            {
                                Message = $"Wall level set to {lvl}.",
                                Name = "DebugManager",
                                ExpLevel = 100,
                                League = 16
                            });
                        }
                        break;
                    }

                    default:
                    {
                        await Resources.Gateway.Send(new GlobalChatLineMessage(Device)
                        {
                            Message = "Invalid Command. Type '/help' for a list of all commands.",
                            Name = "DebugManager",
                            ExpLevel = 100,
                            League = 16
                        });
                        break;
                    }
                }
            else if ((DateTime.UtcNow - Device.LastChatMessage).TotalSeconds >= 1.0)
                if (!string.IsNullOrEmpty(Message))
                {
                    await Resources.ChatManager.Process(new GlobalChatEntry
                    {
                        Message = Message,
                        SenderName = Device.Player.Name,
                        SenderId = Device.Player.AccountId,
                        SenderExpLevel = Device.Player.ExpLevel,
                        SenderLeague = LogicUtils.GetLeagueByScore(Device.Player.Score)
                    });

                    Device.LastChatMessage = DateTime.UtcNow;
                }
        }
    }
}