using RetroClashCore.Files.CsvHelpers;
using RetroGames.Files.CsvReader;

namespace RetroClashCore.Files.Logic
{
    public class Particle_emitters : Data
    {
        public Particle_emitters(Row row, DataTable datatable) : base(row, datatable)
        {
            LoadData(this, GetType(), row);
        }

        public string Name { get; set; }

        public int ParticleCount { get; set; }

        public int EmissionTime { get; set; }

        public int MinLife { get; set; }

        public int MaxLife { get; set; }

        public int MinHorizAngle { get; set; }

        public int MaxHorizAngle { get; set; }

        public int MinVertAngle { get; set; }

        public int MaxVertAngle { get; set; }

        public int MinSpeed { get; set; }

        public int MaxSpeed { get; set; }

        public int StartZ { get; set; }

        public int Gravity { get; set; }

        public int Inertia { get; set; }

        public int StartScale { get; set; }

        public int EndScale { get; set; }

        public int MinRotate { get; set; }

        public int MaxRotate { get; set; }

        public int ParticleFadeOutTime { get; set; }

        public int StartRadius { get; set; }

        public string ParticleSwf { get; set; }

        public string ParticleExportName { get; set; }

        public bool ScaleTimeline { get; set; }

        public bool OrientToMovement { get; set; }

        public bool OrientToParent { get; set; }

        public bool BounceFromGround { get; set; }

        public bool AdditiveBlend { get; set; }

        public int FadeInTime { get; set; }

        public int FadeOutTime { get; set; }
    }
}