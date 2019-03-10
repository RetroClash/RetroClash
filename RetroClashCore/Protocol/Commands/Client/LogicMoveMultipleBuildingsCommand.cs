using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicMoveMultipleBuildingsCommand : LogicCommand
    {
        public LogicMoveMultipleBuildingsCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public int Count { get; set; }

        public override void Decode()
        {
            Count = Reader.ReadInt32();
        }

        public override async Task Process()
        {
            await Task.Run(() =>
            {
                for (var index = 0; index < Count; index++)
                {
                    var x = Reader.ReadInt32();
                    var y = Reader.ReadInt32();

                    Device.Player.LogicGameObjectManager.Move(Reader.ReadInt32(), x, y);
                }

                Reader.ReadInt32();
            });
        }
    }
}