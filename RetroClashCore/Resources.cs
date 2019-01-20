using System;
using RetroClashCore.Database;
using RetroClashCore.Database.Caching;
using RetroClashCore.Files;
using RetroClashCore.Logic.Manager;
using RetroClashCore.Network;
using RetroGames.Helpers;

namespace RetroClashCore
{
    public static class Resources
    {
        private static PlayerDb _playerDb;
        private static ReplayDb _replayDb;
        private static AllianceDb _allianceDb;
        private static Logger _logger;
        private static Redis _redis;

        public static Gateway Gateway { get; set; }
        public static Players PlayerCache { get; set; }
        public static Alliances AllianceCache { get; set; }
        public static Leaderboards LeaderboardCache { get; set; }
        public static Configuration Configuration { get; set; }
        public static Levels Levels { get; set; }
        public static LogicGlobalChatManager ChatManager { get; set; }
        public static Fingerprint Fingerprint { get; set; }
        public static Csv Csv { get; set; }
        public static DateTime StartDateTime { get; set; }

        public static void Construct()
        {
            Configuration = new Configuration();
            Configuration.Initialize();

            _logger = new Logger();

            Logger.Log($"ENV: {(Utils.IsLinux ? "Linux" : "Windows")}");

            Csv = new Csv();
            Fingerprint = new Fingerprint();

            _playerDb = new PlayerDb();
            _replayDb = new ReplayDb();
            _allianceDb = new AllianceDb();

            _redis = new Redis();

            Levels = new Levels();
            PlayerCache = new Players();
            AllianceCache = new Alliances();
            LeaderboardCache = new Leaderboards();

            ChatManager = new LogicGlobalChatManager();

            Gateway = new Gateway();

            StartDateTime = DateTime.UtcNow;

            Gateway.StartAsync().Wait();
        }
    }
}