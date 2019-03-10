using System;
using System.Collections.Generic;
using RetroClash.Protocol.Commands.Client;

namespace RetroClash.Protocol
{
    public class LogicCommandManager
    {
        public static Dictionary<int, Type> Commands;

        static LogicCommandManager()
        {
            Commands = new Dictionary<int, Type>
            {
                {500, typeof(LogicBuyBuildingCommand)},
                {501, typeof(LogicMoveBuildingCommand)},
                {502, typeof(LogicUpgradeBuildingCommand)},
                {503, typeof(LogicSellBuildingCommand)},
                {504, typeof(LogicSpeedUpConstructionCommand)},
                {507, typeof(LogicClearObstacleCommand)},
                {508, typeof(LogicTrainUnitCommand)},
                {510, typeof(LogicBuyTrapCommand)},
                //{511, typeof(LogicRequestAllianceUnitsCommand)},
                {512, typeof(LogicBuyDecoCommand)},
                {516, typeof(LogicUnitUpgradeCommand)},
                {517, typeof(LogicSpeedUpUnitUpgradeCommand)},
                {520, typeof(LogicUnlockBuildingCommand)},
                {521, typeof(LogicFreeWorkerCommand)},
                {522, typeof(LogicBuyShieldCommand)},
                {523, typeof(LogicClaimAchievementRewardCommand)},
                {524, typeof(LogicToggleAttackModeCommand)},
                {525, typeof(LogicLoadTurretCommand)},
                {526, typeof(LogicBoostBuildingCommand)},
                {527, typeof(LogicUpgradeHeroCommand)},
                {528, typeof(LogicSpeedUpHeroUpgradeCommand)},
                {529, typeof(LogicToggleHeroSleepCommand)},
                {530, typeof(LogicSpeedUpHeroHealthCommand)},
                {532, typeof(LogicNewShopItemsSeenCommand)},
                {533, typeof(LogicMoveMultipleBuildingsCommand)},
                {534, typeof(LogicDisbandLeagueCommand)},
                //{535, typeof(LogicChangeLeagueCommand)},
                //{537, typeof(LogicSendAllianceMailCommand)},
                {538, typeof(LogicLeagueNotificationsSeenCommand)},
                {539, typeof(LogicNewsSeenCommand)},
                //{542, typeof(LogicShareReplayCommand)},
                //{543, typeof(LogicElderKickCommand)},
                {544, typeof(LogicEditModeShownCommand)},
                {600, typeof(LogicPlaceAttackerCommand)},
                //{602, typeof(LogicEndAttackPreparationCommand)},
                {603, typeof(LogicEndCombatCommand)},
                {604, typeof(LogicCastSpellCommand)},
                {605, typeof(LogicPlaceHeroCommand)},
                {700, typeof(LogicMatchmakingCommand)},
                {701, typeof(LogicCommandFailed)}
            };
        }
    }
}