using System;
using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroClashCore.Logic.Slots;
using RetroClashCore.Logic.StreamEntry.Alliance;
using RetroClashCore.Protocol.Commands.Server;
using RetroClashCore.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class JoinAllianceMessage : PiranhaMessage
    {
        public JoinAllianceMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public long AllianceId { get; set; }

        public override void Decode()
        {
            AllianceId = Reader.ReadInt64();
        }

        public override async Task Process()
        {
            var alliance = await Resources.AllianceCache.GetAlliance(AllianceId);

            if (alliance != null)
                if (!alliance.IsFull)
                {
                    alliance.Members.Add(new AllianceMember(Device.Player.AccountId, Enums.Role.Member,
                        Device.Player.Score));

                    await Resources.Gateway.Send(new AvailableServerCommandMessage(Device)
                    {
                        Command = await new LogicJoinAlliance(Device)
                        {
                            AllianceId = AllianceId,
                            AllianceBadge = alliance.Badge,
                            AllianceName = alliance.Name,
                            Role = Enums.Role.Member
                        }.Handle()
                    });

                    await Resources.Gateway.Send(new AllianceStreamMessage(Device)
                    {
                        AllianceStream = alliance.Stream
                    });

                    var entry = new AllianceEventStreamEntry
                    {
                        CreationDateTime = DateTime.Now,
                        Id = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                        EventType = Enums.AllianceEvent.JoinedClan,
                        AvatarId = Device.Player.AccountId,
                        AvatarName = Device.Player.Name,
                        SenderRole = alliance.GetRole(Device.Player.AccountId)
                    };

                    entry.SetSender(Device.Player);

                    alliance.AddEntry(entry);

                    foreach (var member in alliance.Members)
                    {
                        var player = await Resources.PlayerCache.GetPlayer(member.AccountId, true);

                        if (player != null)
                            await Resources.Gateway.Send(new AllianceStreamEntryMessage(player.Device)
                            {
                                AllianceStreamEntry = entry
                            });
                    }

                    Device.Player.AllianceId = AllianceId;
                }
                else
                {
                    await Resources.Gateway.Send(new AllianceJoinFailedMessage(Device));
                }
            else
                await Resources.Gateway.Send(new AllianceJoinFailedMessage(Device));
        }
    }
}