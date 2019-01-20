using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class KeepAliveServerMessage : PiranhaMessage
    {
        public KeepAliveServerMessage(Device device) : base(device)
        {
            Id = 20108;
        }
    }
}