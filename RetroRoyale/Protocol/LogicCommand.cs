using System;
using System.IO;
using System.Threading.Tasks;
using RetroRoyale.Logic;
using RetroGames.Helpers;

namespace RetroRoyale.Protocol
{
    public class LogicCommand : IDisposable
    {
        public LogicCommand(Device device)
        {
            Device = device;
            Stream = new MemoryStream();
        }

        public LogicCommand(Device device, Reader reader)
        {
            Device = device;
            Reader = reader;
        }

        public MemoryStream Stream { get; set; }
        public Device Device { get; set; }

        public int Type { get; set; }
        public int SubTick { get; set; }
        public Reader Reader { get; set; }

        public void Dispose()
        {
            Stream = null;
            Device = null;
            Reader = null;
        }

        public virtual void Decode()
        {
        }

        public virtual async Task Encode()
        {
        }

        public virtual async Task Process()
        {
        }

        public async Task<LogicCommand> Handle()
        {
            await Encode();
            return this;
        }
    }
}