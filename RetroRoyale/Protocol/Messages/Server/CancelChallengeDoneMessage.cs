using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class CancelChallengeDoneMessage : PiranhaMessage
    {
        public CancelChallengeDoneMessage(Device device) : base(device)
        {
            Id = 24124;
        }
    }
}