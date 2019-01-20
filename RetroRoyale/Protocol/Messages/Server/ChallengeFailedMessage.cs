using RetroRoyale.Logic;

namespace RetroRoyale.Protocol.Messages.Server
{
    public class ChallengeFailedMessage : PiranhaMessage
    {
        public ChallengeFailedMessage(Device device) : base(device)
        {
            Id = 24121;
        }
    }
}