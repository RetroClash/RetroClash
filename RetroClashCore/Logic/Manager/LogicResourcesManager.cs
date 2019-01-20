using System.Collections.Generic;
using RetroClashCore.Logic.Manager.Items;

namespace RetroClashCore.Logic.Manager
{
    public class LogicResourcesManager : List<Resource>
    {
        public bool AddResource(int resourceType, int value)
        {
            var index = FindIndex(x => x.Id == resourceType);

            if (index <= -1)
                return false;

            this[index].Value += value;

            return true;
        }

        public bool UseResource(int resourceType, int value)
        {
            var index = FindIndex(x => x.Id == resourceType);

            if (index <= -1)
                return false;

            if (this[index].Value < value)
                return false;

            this[index].Value -= value;

            return true;
        }

        public void Initialize()
        {
            Clear();

            Add(new Resource
            {
                Id = 3000001,
                Value = 1000000000
            });

            Add(new Resource
            {
                Id = 3000002,
                Value = 1000000000
            });

            Add(new Resource
            {
                Id = 3000003,
                Value = 100000000
            });
        }
    }
}