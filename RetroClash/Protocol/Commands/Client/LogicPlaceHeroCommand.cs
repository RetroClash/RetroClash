using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Logic.Replay.Items;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicPlaceHeroCommand : LogicCommand
    {
        public LogicPlaceHeroCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public int HeroId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override void Decode()
        {
            X = Reader.ReadInt32(); // X
            Y = Reader.ReadInt32(); // Y

            HeroId = Reader.ReadInt32();

            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            await Task.Run(() =>
            {
                var hero = Device.Player.HeroManager.GetByType(HeroId);

                if (hero != null)
                    hero.Health = 60;

                if (Device.State == Enums.State.Battle)
                    Device.Player.Battle.RecordCommand(new ReplayCommand
                    {
                        CommandType = Type,
                        ReplayCommandInfo = new ReplayCommandInfo
                        {
                            ReplayCommandBase = GetBase(),
                            X = X,
                            Y = Y,
                            Data = HeroId
                        }
                    });
            });
        }
    }
}