using RetroClash.Logic;

namespace RetroClash.Protocol.Messages.Server
{
    public class AllianceListMessage : PiranhaMessage
    {
        public AllianceListMessage(Device device) : base(device)
        {
            Id = 24310;
        }
    }
}