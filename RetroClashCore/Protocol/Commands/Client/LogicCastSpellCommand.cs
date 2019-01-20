using System.Threading.Tasks;
using RetroClashCore.Logic;
using RetroClashCore.Logic.Replay.Items;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicCastSpellCommand : LogicCommand
    {
        public LogicCastSpellCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public int SpellId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override void Decode()
        {
            X = Reader.ReadInt32(); // X
            Y = Reader.ReadInt32(); // Y

            SpellId = Reader.ReadInt32();

            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            await Task.Run(() =>
            {
                var index = Device.Player.Units.Spells.FindIndex(spell => spell.Id == SpellId);

                if (index <= -1) return;
                Device.Player.Units.Spells[index].Count--;

                if (Device.State == Enums.State.Battle)
                    Device.Player.Battle.RecordCommand(new ReplayCommand
                    {
                        CommandType = Type,
                        ReplayCommandInfo = new ReplayCommandInfo
                        {
                            ReplayCommandBase = GetBase(),
                            X = X,
                            Y = Y,
                            Data = SpellId
                        }
                    });
            });
        }
    }
}