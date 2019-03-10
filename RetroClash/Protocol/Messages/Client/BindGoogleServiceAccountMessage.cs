using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Client
{
    public class BindGoogleServiceAccountMessage : PiranhaMessage
    {
        public BindGoogleServiceAccountMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public override void Decode()
        {
            Reader.ReadString();
        }

        public override async Task Process()
        {
            await Resources.Gateway.Send(new GoogleServiceAccountBoundMessage(Device));
        }
    }
}