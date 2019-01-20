using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RetroGames.Helpers;

namespace RetroRoyale.Logic.StreamEntry
{
    public class AvatarStreamEntry
    {
        public virtual async Task Encode(MemoryStream stream)
        {
        }

        public void SetSender(Player player)
        {
        }
    }
}