using System;
using RetroRoyale.Database;
using RetroRoyale.Database.Caching;
using RetroRoyale.Files;
using RetroRoyale.Network;
using RetroGames.Helpers;

namespace RetroRoyale
{
    public static class Resources
    {
        private static PlayerDb _playerDb;
        private static ReplayDb _replayDb;
        private static Logger _logger;
        private static Redis _redis;

        public static Gateway Gateway { get; set; }
        public static Players PlayerCache { get; set; }
        public static Leaderboards LeaderboardCache { get; set; }
        public static Configuration Configuration { get; set; }
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

            _redis = new Redis();

            PlayerCache = new Players();
            LeaderboardCache = new Leaderboards();

            Gateway = new Gateway();

            StartDateTime = DateTime.UtcNow;

            Gateway.StartAsync().Wait();
        }
    }
}