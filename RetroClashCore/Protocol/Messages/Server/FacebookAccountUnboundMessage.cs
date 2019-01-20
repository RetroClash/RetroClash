using RetroClashCore.Logic;

namespace RetroClashCore.Protocol.Messages.Server
{
    public class FacebookAccountUnboundMessage : PiranhaMessage
    {
        public FacebookAccountUnboundMessage(Device device) : base(device)
        {
            Id = 24214;
        }
    }
}