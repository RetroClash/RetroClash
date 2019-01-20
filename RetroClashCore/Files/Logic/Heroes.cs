using RetroClashCore.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files.Logic
{
    public class Heroes : Data
    {
        public Heroes(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string TID { get; set; }

        public string InfoTID { get; set; }

        public string SWF { get; set; }

        public int Speed { get; set; }

        public int Hitpoints { get; set; }

        public int UpgradeTimeH { get; set; }

        public string UpgradeResource { get; set; }

        public int UpgradeCost { get; set; }

        public int RequiredTownHallLevel { get; set; }

        public int AttackRange { get; set; }

        public int AttackSpeed { get; set; }

        public int Damage { get; set; }

        public int PreferedTargetDamageMod { get; set; }

        public int DamageRadius { get; set; }

        public string IconSWF { get; set; }

        public string IconExportName { get; set; }

        public string BigPicture { get; set; }

        public string Projectile { get; set; }

        public string PreferedTargetBuilding { get; set; }

        public string DeployEffect { get; set; }

        public string AttackEffect { get; set; }

        public string HitEffect { get; set; }

        public bool IsFlying { get; set; }

        public bool AirTargets { get; set; }

        public bool GroundTargets { get; set; }

        public int AttackCount { get; set; }

        public string DieEffect { get; set; }

        public string Animation { get; set; }

        public int MaxSearchRadiusForDefender { get; set; }

        public int HousingSpace { get; set; }

        public string SpecialAbilityEffect { get; set; }

        public int RegenerationTimeMinutes { get; set; }

        public int TrainingTime { get; set; }

        public string TrainingResource { get; set; }

        public int TrainingCost { get; set; }

        public string CelebrateEffect { get; set; }

        public int SleepOffsetX { get; set; }

        public int SleepOffsetY { get; set; }

        public int PatrolRadius { get; set; }
    }
}