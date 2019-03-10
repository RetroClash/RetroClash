using System.Collections.Generic;
using RetroClash.Logic.Manager.Items;

namespace RetroClash.Logic.Manager
{
    public class Achievements : List<Achievement>
    {
        public new void Add(Achievement achievement)
        {
            if (!Contains(achievement))
                base.Add(achievement);
        }
    }
}