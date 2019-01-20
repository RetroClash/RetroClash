using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroRoyale.Logic;
using StackExchange.Redis;

namespace RetroRoyale.Database
{
    public class Redis
    {
        private static IDatabase _players;
        private static IServer _server;

        private static ConnectionMultiplexer _connection;

        public static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.None
        };

        public Redis()
        {
            try
            {
                var config = new ConfigurationOptions
                {
                    AllowAdmin = true,
                    ConnectTimeout = 10000,
                    ConnectRetry = 10,
                    HighPrioritySocketThreads = true,
                    Password = Resources.Configuration.RedisPassword
                };

                config.EndPoints.Add(Resources.Configuration.RedisServer, 6379);

                _connection = ConnectionMultiplexer.Connect(config);

                _players = _connection.GetDatabase(2);
                _server = _connection.GetServer(Resources.Configuration.RedisServer, 6379);
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }

        public static bool IsConnected => _server != null;

        public static async Task CachePlayer(Player player)
        {
            try
            {
                await _players.StringSetAsync(player.AccountId.ToString(),
                    JsonConvert.SerializeObject(player, Settings), TimeSpan.FromHours(4));
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }

        public static async Task UncachePlayer(long id)
        {
            try
            {
                await _players.KeyDeleteAsync(id.ToString());
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }

        public static async Task<Player> GetCachedPlayer(long id)
        {
            try
            {
                var profile = await _players.StringGetAsync(id.ToString());

                if (!string.IsNullOrEmpty(profile))
                {
                    return JsonConvert.DeserializeObject<Player>(profile,
                            Settings);
                }

                var avatar = await PlayerDb.Get(id);
                await CachePlayer(avatar);
                return avatar;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }

            return null;
        }

        public static async Task<Player> GetRandomCachedPlayer()
        {
            return await GetCachedPlayer(Convert.ToInt64((await _players.KeyRandomAsync()).ToString()));
        }

        public static int CachedPlayers()
        {
            try
            {
                return Convert.ToInt32(
                    _connection.GetServer(Resources.Configuration.RedisServer, 6379).Info("keyspace")[0]
                        .ElementAt(_players.Database)
                        .Value
                        .Split(new[] {"keys="}, StringSplitOptions.None)[1]
                        .Split(new[] {",expires="}, StringSplitOptions.None)[0]);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}