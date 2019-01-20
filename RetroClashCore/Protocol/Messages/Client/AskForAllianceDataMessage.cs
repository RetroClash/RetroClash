using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroClashCore.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class AskForAllianceDataMessage : PiranhaMessage
    {
        public AskForAllianceDataMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public long AllianceId { get; set; }

        public override void Decode()
        {
            AllianceId = Reader.ReadInt64();
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new AllianceDataMessage(Device)
            {
                AllianceId = AllianceId
            });
        }
    }
}