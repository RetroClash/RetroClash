using System.Threading.Tasks;
using RetroClashCore.Files;
using RetroClashCore.Files.Logic;
using RetroClashCore.Logic;
using RetroClashCore.Logic.Manager.Items;
using RetroGames.Helpers;

namespace RetroClashCore.Protocol.Commands.Client
{
    public class LogicClaimAchievementRewardCommand : LogicCommand
    {
        public LogicClaimAchievementRewardCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public int AchievementId { get; set; }

        public override void Decode()
        {
            AchievementId = Reader.ReadInt32();
            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            await Task.Run(() =>
            {
                var achievement =
                    (Achievements) Csv.Tables.Get(Enums.Gamefile.Achievements).GetDataWithId(AchievementId);

                Device.Player.Achievements.Add(new Achievement
                {
                    Id = AchievementId,
                    Data = achievement.ActionCount
                });

                Device.Player.AddDiamonds(achievement.DiamondReward);
            });
        }
    }
}