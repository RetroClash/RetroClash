using RetroClashCore.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files.Logic
{
    public class Resources : Data
    {
        public Resources(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string TID { get; set; }

        public string SWF { get; set; }

        public string CollectEffect { get; set; }

        public string ResourceIconExportName { get; set; }

        public string StealEffect { get; set; }

        public bool PremiumCurrency { get; set; }

        public string HudInstanceName { get; set; }

        public string CapFullTID { get; set; }

        public int TextRed { get; set; }

        public int TextGreen { get; set; }

        public int TextBlue { get; set; }

        public int CombinedResourceStorageMaxLoot { get; set; }
    }
}