using RetroClashCore.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files.Logic
{
    public class News : Data
    {
        public News(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public int ID { get; set; }

        public bool Enabled { get; set; }

        public bool EnabledIOS { get; set; }

        public bool EnabledAndroid { get; set; }

        public string TID { get; set; }

        public string InfoTID { get; set; }

        public string ButtonTID { get; set; }

        public string ButtonURL { get; set; }

        public string ItemSWF { get; set; }

        public string ItemExportName { get; set; }

        public string IconSWF { get; set; }

        public string IconExportName { get; set; }

        public string IncludedCountries { get; set; }

        public string ExcludedCountries { get; set; }

        public int MinLevel { get; set; }

        public int MaxDiamonds { get; set; }

        public bool ClickToDismiss { get; set; }
    }
}