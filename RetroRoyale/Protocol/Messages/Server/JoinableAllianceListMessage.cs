using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class JoinableAllianceListMessage : PiranhaMessage
    {
        public JoinableAllianceListMessage(Device device) : base(device)
        {
            Id = 24304;
        }

        public override async Task Encode()
        {
            await Stream.WriteVInt(0);
        }
    }
}