using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Commands.Server
{
    public class LogicDiamondsAdded : LogicCommand
    {
        public LogicDiamondsAdded(Device device) : base(device)
        {
            Type = 7;
        }

        public override async Task Encode()
        {
            await Stream.WriteInt(0); // Free Diamonds
            await Stream.WriteInt(0); // Ammount
            await Stream.WriteString("G:0"); // TransactionId
        }
    }
}