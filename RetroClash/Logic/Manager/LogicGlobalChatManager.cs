using System;
using System.Threading.Tasks;
using RetroClash.Logic.Manager.Items;
using RetroClash.Protocol.Messages.Server;

namespace RetroClash.Logic.Manager
{
    public class LogicGlobalChatManager
    {
        public async Task Process(GlobalChatEntry entry)
        {
            foreach (var player in Resources.PlayerCache.Values)
                if (player.Device != null)
                    await Resources.Gateway.Send(new GlobalChatLineMessage(player.Device)
                    {
                        AccountId = entry.SenderId,
                        Message = entry.Message,
                        ExpLevel = entry.SenderExpLevel,
                        League = entry.SenderLeague,
                        Name = entry.SenderName
                    });
                else
                    await Resources.PlayerCache.RemovePlayer(player.AccountId, Guid.Empty, true);
        }
    }
}