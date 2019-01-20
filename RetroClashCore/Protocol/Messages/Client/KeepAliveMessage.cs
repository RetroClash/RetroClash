using System;
using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroClashCore.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Messages.Client
{
    public class KeepAliveMessage : PiranhaMessage
    {
        public KeepAliveMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public override async Task Process()
        {
            Device.LastKeepAlive = DateTime.UtcNow;

            await Resources.Gateway.Send(new KeepAliveOk(Device));
        }
    }
}