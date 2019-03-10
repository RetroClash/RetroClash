using System.Collections.Generic;
using RetroClash.Logic.Manager.Items;

namespace RetroClash.Logic.Manager
{
    public class LogicHeroManager : List<Hero>
    {
        public void Add(int id, int buildingid)
        {
            var index = FindIndex(x => x.Id == id);

            if (index <= -1)
                Add(new Hero
                {
                    Id = id,
                    BuildingId = buildingid,
                    State = 3
                });
        }

        public Hero Get(int id)
        {
            var index = FindIndex(x => x.BuildingId == id);

            return index > -1 ? this[index] : null;
        }

        public Hero GetByType(int id)
        {
            var index = FindIndex(x => x.Id == id);

            return index > -1 ? this[index] : null;
        }
    }
}