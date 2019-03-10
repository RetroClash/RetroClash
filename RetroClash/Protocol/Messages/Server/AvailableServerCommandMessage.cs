using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Server
{
    public class AvailableServerCommandMessage : PiranhaMessage
    {
        public AvailableServerCommandMessage(Device device) : base(device)
        {
            Id = 24111;
        }

        public LogicCommand Command { get; set; }

        public override async Task Encode()
        {
            await Stream.WriteInt(Command.Type);
            await Stream.WriteBuffer(Command.Stream.ToArray());
        }
    }
}