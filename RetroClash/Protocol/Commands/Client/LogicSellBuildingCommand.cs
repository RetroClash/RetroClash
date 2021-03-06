﻿using System.Threading.Tasks;
using RetroClash.Logic;
using RetroGames.Helpers;

namespace RetroClash.Protocol.Commands.Client
{
    public class LogicSellBuildingCommand : LogicCommand
    {
        public LogicSellBuildingCommand(Device device, Reader reader) : base(device, reader)
        {
        }

        public int BuildingId { get; set; }

        public override void Decode()
        {
            BuildingId = Reader.ReadInt32();
            Reader.ReadInt32();
        }

        public override async Task Process()
        {
            await Task.Run(() => { Device.Player.LogicGameObjectManager.RemoveDeco(BuildingId); });
        }
    }
}