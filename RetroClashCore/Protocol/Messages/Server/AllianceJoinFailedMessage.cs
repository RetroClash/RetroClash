using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class AllianceJoinFailedMessage : PiranhaMessage
    {
        public AllianceJoinFailedMessage(Device device) : base(device)
        {
            Id = 24302;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(1);
        }
    }
}