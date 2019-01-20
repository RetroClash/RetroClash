using RetroClashCore.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files.Logic
{
    public class Leagues : Data
    {
        public Leagues(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string TID { get; set; }

        public string TIDShort { get; set; }

        public string IconSWF { get; set; }

        public string IconExportName { get; set; }

        public string LeagueBannerIcon { get; set; }

        public string LeagueBannerIconNum { get; set; }

        public int GoldReward { get; set; }

        public int ElixirReward { get; set; }

        public int DarkElixirReward { get; set; }

        public int PlacementLimitLow { get; set; }

        public int PlacementLimitHigh { get; set; }

        public int DemoteLimit { get; set; }

        public int PromoteLimit { get; set; }

        public int BucketPlacementRangeLow { get; set; }

        public int BucketPlacementRangeHigh { get; set; }

        public int BucketPlacementSoftLimit { get; set; }

        public int BucketPlacementHardLimit { get; set; }

        public bool IgnoredByServer { get; set; }

        public bool DemoteEnabled { get; set; }

        public bool PromoteEnabled { get; set; }
    }
}