using RetroClashCore.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files.Logic
{
    public class Traps : Data
    {
        public Traps(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string TID { get; set; }

        public string InfoTID { get; set; }

        public string SWF { get; set; }

        public string ExportName { get; set; }

        public int Damage { get; set; }

        public int DamageRadius { get; set; }

        public int TriggerRadius { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Effect { get; set; }

        public string Effect2 { get; set; }

        public string DamageEffect { get; set; }

        public bool Passable { get; set; }

        public string BuildResource { get; set; }

        public int BuildCost { get; set; }

        public bool EjectVictims { get; set; }

        public int MinTriggerHousingLimit { get; set; }

        public int EjectHousingLimit { get; set; }

        public string ExportNameTriggered { get; set; }

        public int ActionFrame { get; set; }

        public string PickUpEffect { get; set; }

        public string PlacingEffect { get; set; }

        public string AppearEffect { get; set; }

        public int DurationMS { get; set; }

        public int SpeedMod { get; set; }

        public int DamageMod { get; set; }

        public bool AirTrigger { get; set; }

        public bool GroundTrigger { get; set; }

        public int HitDelayMS { get; set; }

        public int HitCnt { get; set; }

        public string Projectile { get; set; }
    }
}