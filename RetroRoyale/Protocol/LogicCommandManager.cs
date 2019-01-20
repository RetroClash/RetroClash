using System;
using System.Collections.Generic;
using RetroRoyale.Protocol.Commands.Client;

namespace RetroRoyale.Protocol
{
    public class LogicCommandManager
    {
        public static Dictionary<int, Type> Commands;

        static LogicCommandManager()
        {
            Commands = new Dictionary<int, Type>
            {
                //{500, typeof(LogicSwapSpellsCommand)},
                //{501, typeof(LogicSelectDeckCommand)},
                //{502, typeof(LogicStartExploringCommand)},
                //{503, typeof(LogicStartRewardClaimCommand)},
                //{504, typeof(LogicFuseSpellsCommand)},
                //{506, typeof(LogicSpeedUpExploringCommand)},
                //{507, typeof(LogicSellChestCommand)},
                //{508, typeof(LogicGambleCommand)},
                //{509, typeof(LogicCompleteTutorialHomeCommand)},
                //{513, typeof(LogicFreeWorkerCommand)},
                //{514, typeof(LogicBuyResourcesCommand)},
                //{515, typeof(LogicOfferChestForCoOpenCommand)},
                //{516, typeof(LogicUpdateLastShownLevelUpCommand)},
                //{517, typeof(LogicCancelChestOpenCommand)},
                //{518, typeof(LogicCollectFreeChestCommand)},
                //{519, typeof(LogicUpgradeTowerCommand)},
                //{520, typeof(LogicSortCollectionCommand)},
                //{521, typeof(LogicMoveSpellCommand)},
                //{522, typeof(LogicClearChestSourceCommand)},
                //{523, typeof(LogicCollectMultiWinChestCommand)},
                //{524, typeof(LogicRequestSpellsCommand)},
                //{525, typeof(LogicSpellPageOpenedCommand)},
                //{526, typeof(LogicKickAllianceMemberCommand)},
                //{527, typeof(LogicShareReplayCommand)},
                //{531, typeof(LogicHelpOpenedCommand)},
                //{532, typeof(LogicShopOpenedCommand)},
                //{533, typeof(LogicSendAllianceMailCommand)},
                //{534, typeof(LogicChallengeCommand)},
                {536, typeof(LogicTvReplaySeenCommand)},
                {537, typeof(LogicChallengeCommand)},
                //{538, typeof(LogicRefreshAchievementsCommand)},
                //{539, typeof(LogicPageOpenedCommand)},
                //{540, typeof(LogicUpdateLastTournamentCommand)},
            };
        }
    }
}