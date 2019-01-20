using System;
using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;
using RetroRoyale.Protocol.Messages.Server;

namespace RetroRoyale.Protocol.Messages.Client
{
    public class KeepAliveMessage : PiranhaMessage
    {
        public KeepAliveMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            Device.LastKeepAlive = DateTime.UtcNow;

            await Resources.Gateway.Send(new KeepAliveServerMessage(Device));
        }
    }
}