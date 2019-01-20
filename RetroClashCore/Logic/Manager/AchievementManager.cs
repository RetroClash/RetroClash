using System.Collections.Generic;
using RetroClashCore.Logic.Manager.Items;

namespace RetroClashCore.Logic.Manager
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