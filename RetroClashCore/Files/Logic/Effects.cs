using RetroClashCore.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files.Logic
{
    public class Effects : Data
    {
        public Effects(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public string SWF { get; set; }

        public string ExportName { get; set; }

        public string ParticleEmitter { get; set; }

        public int CameraShake { get; set; }

        public bool AttachToParent { get; set; }

        public bool Looping { get; set; }

        public string IsoLayer { get; set; }

        public bool Targeted { get; set; }

        public int MaxCount { get; set; }

        public string Sound { get; set; }

        public int Volume { get; set; }

        public int MinPitch { get; set; }

        public int MaxPitch { get; set; }

        public string LowEndSound { get; set; }

        public int LowEndVolume { get; set; }

        public int LowEndMinPitch { get; set; }

        public int LowEndMaxPitch { get; set; }

        public bool Beam { get; set; }
    }
}