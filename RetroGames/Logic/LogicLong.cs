using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroGames.Helpers;

namespace RetroGames.Logic
{
    public struct LogicLong
    {
        [JsonIgnore]
        public int High => Convert.ToInt32(Long >> 32);

        [JsonIgnore]
        public int Low => (int) Long;

        public LogicLong(int high, int low)
        {
            Long = (high << 32) | low;
        }

        public LogicLong(long value)
        {
            Long = value;
        }

        [JsonIgnore]
        public long Long { get; set; }

        public async Task Encode(MemoryStream stream)
        {
            await stream.WriteInt(High);
            await stream.WriteInt(Low);
        }

        public static implicit operator LogicLong(long Long)
        {
            return new LogicLong(Long);
        }

        public static implicit operator long(LogicLong logicLong)
        {
            return logicLong.Long;
        }

        public override string ToString()
        {
            return $"LogicLong({High}, {Low})";
        }

        public bool IsZero()
        {
            return High == 0 && Low == 0;
        }
    }
}