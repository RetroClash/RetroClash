using RetroClashCore.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files.Logic
{
    public class Characters : Data
    {
        public Characters(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string TID { get; set; }

        public string InfoTID { get; set; }

        public string SWF { get; set; }

        public int HousingSpace { get; set; }

        public int BarrackLevel { get; set; }

        public int LaboratoryLevel { get; set; }

        public int Speed { get; set; }

        public int Hitpoints { get; set; }

        public int TrainingTime { get; set; }

        public string TrainingResource { get; set; }

        public int TrainingCost { get; set; }

        public int UpgradeTimeH { get; set; }

        public string UpgradeResource { get; set; }

        public int UpgradeCost { get; set; }

        public int AttackRange { get; set; }

        public int AttackSpeed { get; set; }

        public int Damage { get; set; }

        public int PreferedTargetDamageMod { get; set; }

        public int DamageRadius { get; set; }

        public bool SelfAsAoeCenter { get; set; }

        public string IconSWF { get; set; }

        public string IconExportName { get; set; }

        public string BigPicture { get; set; }

        public string Projectile { get; set; }

        public string PreferedTargetBuilding { get; set; }

        public string PreferedTargetBuildingClass { get; set; }

        public string DeployEffect { get; set; }

        public string AttackEffect { get; set; }

        public string HitEffect { get; set; }

        public bool IsFlying { get; set; }

        public bool AirTargets { get; set; }

        public bool GroundTargets { get; set; }

        public int AttackCount { get; set; }

        public string DieEffect { get; set; }

        public string Animation { get; set; }

        public int UnitOfType { get; set; }

        public bool IsJumper { get; set; }

        public int MovementOffsetAmount { get; set; }

        public int MovementOffsetSpeed { get; set; }

        public string TombStone { get; set; }

        public int DieDamage { get; set; }

        public int DieDamageRadius { get; set; }

        public string DieDamageEffect { get; set; }

        public int DieDamageDelay { get; set; }

        public bool DisableProduction { get; set; }

        public string SecondaryTroop { get; set; }

        public bool IsSecondaryTroop { get; set; }

        public int SecondaryTroopCnt { get; set; }

        public int SecondarySpawnDist { get; set; }

        public string SummonTroop { get; set; }

        public int SummonTroopCount { get; set; }

        public int SummonCooldown { get; set; }

        public string SummonEffect { get; set; }

        public int SummonLimit { get; set; }

        public int SpawnIdle { get; set; }
    }
}