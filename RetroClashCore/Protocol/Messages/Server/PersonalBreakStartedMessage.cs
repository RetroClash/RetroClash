using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Server
{
    public class PersonalBreakStartedMessage : PiranhaMessage
    {
        public PersonalBreakStartedMessage(Device device) : base(device)
        {
            Id = 20171;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(10); // Seconds?
        }
    }
}