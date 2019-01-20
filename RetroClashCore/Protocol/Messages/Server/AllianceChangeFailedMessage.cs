using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class AllianceChangeFailedMessage : PiranhaMessage
    {
        public AllianceChangeFailedMessage(Device device) : base(device)
        {
            Id = 24333;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(0);
        }
    }
}