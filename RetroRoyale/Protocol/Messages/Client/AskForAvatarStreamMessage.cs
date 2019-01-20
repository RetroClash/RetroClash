using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Client
{
    public class AskForAvatarStreamMessage : PiranhaMessage
    {
        public AskForAvatarStreamMessage(Device device, Reader reader) : base(device, reader)
        {
        }
    }
}