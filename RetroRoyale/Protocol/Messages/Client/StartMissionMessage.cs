using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol.Messages.Client
{
    public class StartMissionMessage : PiranhaMessage
    {
        public StartMissionMessage(Device device, Reader reader) : base(device, reader)
        {
        }
    }
}