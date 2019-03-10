using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Server
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