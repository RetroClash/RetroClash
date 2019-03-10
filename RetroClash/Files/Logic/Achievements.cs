using RetroClash.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClash.Files.Logic
{
    public class Achievements : Data
    {
        public Achievements(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public int Level { get; set; }

        public string TID { get; set; }

        public string InfoTID { get; set; }

        public string Action { get; set; }

        public int ActionCount { get; set; }

        public string ActionData { get; set; }

        public int ExpReward { get; set; }

        public int DiamondReward { get; set; }

        public string IconSWF { get; set; }

        public string IconExportName { get; set; }

        public string CompletedTID { get; set; }

        public bool ShowValue { get; set; }

        public string AndroidID { get; set; }
    }
}