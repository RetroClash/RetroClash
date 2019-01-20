using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using RetroClashCore.Logic;

namespace RetroClashCore.Database.Caching
{
    public class Alliances : ConcurrentDictionary<long, Alliance>
    {
        public Timer Timer = new Timer(10000)
        {
            AutoReset = true
        };

        public Alliances()
        {
            Timer.Elapsed += TimerCallback;
            Timer.Start();
        }

        public bool AddAlliance(Alliance alliance)
        {
            try
            {
                if (ContainsKey(alliance.Id))
                    return true;

                alliance.Timer.Elapsed += alliance.SaveCallback;
                alliance.Timer.Start();

                return TryAdd(alliance.Id, alliance);
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
                return false;
            }
        }

        public async Task<Alliance> GetAlliance(long id)
        {
            if (ContainsKey(id))
            {
                TryGetValue(id, out var value);
                return value;
            }

            Alliance alliance;

            if (Redis.IsConnected)
                alliance = await Redis.GetCachedAlliance(id);
            else
                alliance = await AllianceDb.Get(id);

            AddAlliance(alliance);

            return alliance;
        }

        public async Task<bool> RemoveAlliance(long id)
        {
            try
            {
                if (!ContainsKey(id)) return true;

                var alliance = this[id];

                alliance.Timer.Stop();

                if (Redis.IsConnected)
                    await Redis.CacheAlliance(alliance);

                await AllianceDb.Save(alliance);

                return TryRemove(id, out var value);
            }
            catch (Exception exception)
            {
                Logger.Log(exception, Enums.LogType.Error);
                return false;
            }
        }

        private async void TimerCallback(object state, ElapsedEventArgs args)
        {
            foreach (var alliance in Values)
                if (alliance.Members.Sum(x => x.IsOnline ? 1 : 0) <= 0)
                    await RemoveAlliance(alliance.Id);
        }
    }
}