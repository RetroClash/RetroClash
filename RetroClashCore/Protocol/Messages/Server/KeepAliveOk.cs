using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class KeepAliveOk : PiranhaMessage
    {
        public KeepAliveOk(Device device) : base(device)
        {
            Id = 20108;
        }
    }
}