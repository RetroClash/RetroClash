using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicBuyTrapCommand : LogicCommand
    {
        public LogicBuyTrapCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public int TrapId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override void Decode()
        {
            X = Reader.ReadInt32();
            Y = Reader.ReadInt32();
            TrapId = Reader.ReadInt32();
        }

        public override async Task Process()
        {
            await Task.Run(() => { Device.Player.LogicGameObjectManager.AddTrap(TrapId, X, Y); });
        }
    }
}