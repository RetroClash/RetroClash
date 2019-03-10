using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RetroClash.Logic;

namespace RetroClash.Core.Database
{
    public class AllianceDb
    {
        private static string _connectionString;
        private static long _allianceSeed;

        public static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.None
        };

        public AllianceDb()
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

            _allianceSeed = MaxAllianceId();
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

        public static long MaxAllianceId()
        {
            #region MaxAllianceId

            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var cmd = new MySqlCommand("SELECT coalesce(MAX(Id), 0) FROM clan", connection))
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

        public static async Task<long> AllianceCount()
        {
            #region AllianceCount

            try
            {
                long seed;

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM clan", connection))
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

        public static async Task<Alliance> Create()
        {
            try
            {
                var id = _allianceSeed++;
                if (id <= -1)
                    return null;

                var alliance = new Alliance(id + 1);

                using (var cmd = new MySqlCommand(
                    $"INSERT INTO clan (`Id`, `Name`, `Score`, `IsFull`, `Data`) VALUES ({alliance.Id}, @name, {alliance.Score}, false, @data)")
                )
                {
#pragma warning disable 618
                    cmd.Parameters?.AddWithValue("@name", alliance.Name);
                    cmd.Parameters?.AddWithValue("@data", JsonConvert.SerializeObject(alliance, Settings));
#pragma warning restore 618

                    await ExecuteAsync(cmd);
                }

                return alliance;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return null;
            }
        }

        public static async Task<List<Alliance>> GetGlobalAllianceRanking()
        {
            var list = new List<Alliance>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand("SELECT * FROM clan ORDER BY `Score` DESC LIMIT 200",
                        connection))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            var alliance = JsonConvert.DeserializeObject<Alliance>((string) reader["Data"], Settings);

                            list.Add(alliance);
                        }
                    }

                    await connection.CloseAsync();
                }

                return list;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return list;
            }
        }

        public static async Task<List<Alliance>> GetJoinableAlliances(int limit)
        {
            var list = new List<Alliance>();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var cmd = new MySqlCommand($"SELECT * FROM clan WHERE IsFull = '0' LIMIT {limit}",
                        connection))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            var alliance = JsonConvert.DeserializeObject<Alliance>((string) reader["Data"], Settings);

                            list.Add(alliance);
                        }
                    }

                    await connection.CloseAsync();
                }

                return list;
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return list;
            }
        }

        public static async Task<Alliance> Get(long id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    Alliance alliance = null;

                    using (var cmd = new MySqlCommand($"SELECT * FROM clan WHERE Id = '{id}'", connection))
                    {
                        var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                            alliance = JsonConvert.DeserializeObject<Alliance>((string) reader["Data"], Settings);
                    }

                    await connection.CloseAsync();

                    return alliance;
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);

                return null;
            }
        }

        public static async Task Save(Alliance alliance)
        {
            try
            {
                using (var cmd = new MySqlCommand(
                    $"UPDATE clan SET `Name`=@name, `Score`='{alliance.Score}', `IsFull`=@isFull, `Data`=@data WHERE Id = '{alliance.Id}'")
                )
                {
#pragma warning disable 618
                    cmd.Parameters?.AddWithValue("@name", alliance.Name);
                    cmd.Parameters?.AddWithValue("@isFull", alliance.IsFull);
                    cmd.Parameters?.AddWithValue("@data", JsonConvert.SerializeObject(alliance, Settings));
#pragma warning restore 618

                    await ExecuteAsync(cmd);
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
            }
        }

        public static async Task Delete(long id)
        {
            try
            {
                using (var cmd = new MySqlCommand(
                    $"DELETE FROM clan WHERE Id = '{id}'")
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