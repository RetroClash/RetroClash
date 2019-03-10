using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicEditModeShownCommand : LogicCommand
    {
        public LogicEditModeShownCommand(Device device, Reader reader) : base(device, reader)
        {
        }
    }
}