using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class OutOfSyncMessage : PiranhaMessage
    {
        public OutOfSyncMessage(Device device) : base(device)
        {
            Id = 24104;
        }

        public override async Task Encode()
        {
            await Stream.WriteVInt(0);
            await Stream.WriteVInt(0);
            await Stream.WriteVInt(0);
        }
    }
}