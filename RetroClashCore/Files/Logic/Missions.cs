using RetroClashCore.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files.Logic
{
    public class Missions : Data
    {
        public Missions(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string Dependencies { get; set; }

        public bool Deprecated { get; set; }

        public bool OpenAchievements { get; set; }

        public string BuildBuilding { get; set; }

        public int BuildBuildingLevel { get; set; }

        public int BuildBuildingCount { get; set; }

        public string DefendNPC { get; set; }

        public string AttackNPC { get; set; }

        public bool AttackPlayer { get; set; }

        public bool ChangeName { get; set; }

        public int Delay { get; set; }

        public int TrainTroops { get; set; }

        public bool ShowMap { get; set; }

        public string TutorialText { get; set; }

        public int TutorialStep { get; set; }

        public bool Darken { get; set; }

        public string TutorialTextBox { get; set; }

        public string SpeechBubble { get; set; }

        public bool RightAlignTextBox { get; set; }

        public string ButtonText { get; set; }

        public string TutorialMusic { get; set; }

        public string TutorialSound { get; set; }

        public string RewardResource { get; set; }

        public int RewardResourceCount { get; set; }

        public int RewardXP { get; set; }

        public string RewardTroop { get; set; }

        public int RewardTroopCount { get; set; }

        public int CustomData { get; set; }
    }
}