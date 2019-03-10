using RetroClash.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClash.Files.Logic
{
    public class Building_classes : Data
    {
        public Building_classes(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string TID { get; set; }

        public bool CanBuy { get; set; }

        public bool ShopCategoryResource { get; set; }

        public bool ShopCategoryArmy { get; set; }

        public bool ShopCategoryDefense { get; set; }
    }
}