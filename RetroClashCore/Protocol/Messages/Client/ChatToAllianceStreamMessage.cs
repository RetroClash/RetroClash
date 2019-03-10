using System;
using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Logic.StreamEntry.Alliance;
using RetroClash.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Client
{
    public class ChatToAllianceStreamMessage : PiranhaMessage
    {
        public ChatToAllianceStreamMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public string Message { get; set; }

        public override void Decode()
        {
            Message = Reader.ReadString();
        }

        public override async Task Process()
        {
            var alliance = await Resources.AllianceCache.GetAlliance(Device.Player.AllianceId);

            if (alliance != null)
            {
                var entry = new ChatStreamEntry
                {
                    CreationDateTime = DateTime.Now,
                    Id = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                    Message = Message,
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
            }
        }
    }
}