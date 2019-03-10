using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AllianceJoinOkMessage : PiranhaMessage
    {
        public AllianceJoinOkMessage(Device device) : base(device)
        {
            Id = 24303;
        }
    }
}