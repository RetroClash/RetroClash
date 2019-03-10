using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RetroClash.Logic;
using RetroClash.Logic.Battle;

namespace RetroClash.Core.Database
{
    public class ReplayDb
    {
        private static string _connectionString;
        private static long _replaySeed;

        public static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.None
        };

        public ReplayDb()
        {
            _connectionString = new MySqlConnectionStringBuilder
            {
                Server = Resources.Configuration.MySqlServer,
                Database = Resources.Configuration.MySqlDatabase,
                UserID = Resources.Configuration.MySqlUserId,
                Password = Resources.Configuration.MySqlPassword,
                SslMode = MySqlSslMode.None,
                MinimumPoolSize = 4,
                MaximumPoolSize = 20
            }.ToString();

            _replaySeed = MaxReplayId();
        }

        public static async Task<long> ReplayCount()
        {
            #region ReplayCount

            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM replay", connection))
                    {
                        seed = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    }

                    await connection.CloseAsync();
                }

                return seed;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return 0;
            }

            #endregion
        }

        public static async Task ExecuteAsync(MySqlCommand cmd)
        {
            try
            {
                cmd.Connection = new MySqlConnection(_connectionString);
                await cmd.Connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (MySqlException exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
            finally
            {
                cmd.Connection?.Close();
            }
        }

        public static long MaxReplayId()
        {
            #region MaxReplayId

            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var cmd = new MySqlCommand("SELECT coalesce(MAX(Id), 0) FROM replay", connection))
                    {
                        seed = Convert.ToInt64(cmd.ExecuteScalar());
                    }

                    connection.Close();
                }

                return seed;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return -1;
            }

            #endregion
        }

        public static async Task<string> Get(long id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string data = null;

                    using (var cmd = new MySqlCommand($"SELECT * FROM replay WHERE Id = '{id}'", connection))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                            data = reader["Data"].ToString();
                    }

                    await connection.CloseAsync();

                    return data;
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return null;
            }
        }

        public static async Task<string> GetRandom()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string data = null;

                    using (var cmd = new MySqlCommand("SELECT * FROM replay ORDER BY RAND() LIMIT 1", connection))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                            data = reader["Data"].ToString();
                    }

                    await connection.CloseAsync();

                    return data;
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return null;
            }
        }

        public static async Task<long> Save(string battle)
        {
            try
            {
                var id = _replaySeed++;
                if (id <= -1)
                    return -1;

                using (var cmd = new MySqlCommand(
                    $"INSERT INTO replay (`Id`, `Data`) VALUES ({id + 1}, @data)")
                )
                {
#pragma warning disable 618
                    cmd.Parameters?.AddWithValue("@data", battle);
#pragma warning restore 618

                    await ExecuteAsync(cmd);

                    return id + 1;
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
                return -1;
            }
        }

        public static async Task Delete(long id)
        {
            try
            {
                using (var cmd = new MySqlCommand(
                    $"DELETE FROM replay WHERE Id = '{id}'")
                )
                {
                    await ExecuteAsync(cmd);

                    if (Redis.IsConnected)
                        await Redis.UncachePlayer(id);
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }
    }
}