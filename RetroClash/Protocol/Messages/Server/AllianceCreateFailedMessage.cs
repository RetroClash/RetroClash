using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Server
{
    public class AllianceCreateFailedMessage : PiranhaMessage
    {
        public AllianceCreateFailedMessage(Device device) : base(device)
        {
            Id = 24332;
        }

        // Error Codes:
        // 1 = Invalid Name
        // 2 = Invalid Description
        // 3 = Name to short

        public override async Task Encode()
        {
            await Stream.WriteInt(1);
        }
    }
}