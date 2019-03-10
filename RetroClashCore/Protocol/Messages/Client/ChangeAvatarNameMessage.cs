using System.Threading.Tasks;
using RetroClash.Logic;
using RetroClash.Protocol.Commands.Server;
using RetroClash.Protocol.Messages.Server;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Messages.Client
{
    public class ChangeAvatarNameMessage : PiranhaMessage
    {
        public ChangeAvatarNameMessage(Device device, Reader reader) : base(device, reader)
        {
            Save = true;
        }

        public string Name { get; set; }

        public override void Decode()
        {
            Name = Reader.ReadString();
        }

        public override async Task Process()
        {
            if (Name.Length >= 3 && Name.Length <= 15)
            {
                Device.Player.Name = Name;
                Device.Player.TutorialSteps = 13;
                Device.Player.ExpLevel = 100;

                await Resources.Gateway.Send(new AvailableServerCommandMessage(Device)
                {
                    Command = await new LogicChangeAvatarName(Device)
                    {
                        AvatarName = Name
                    }.Handle()
                });
            }
            else
            {
                await Resources.Gateway.Send(new AvatarNameChangeFailedMessage(Device));
            }
        }
    }
}