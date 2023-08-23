using Pewpew.Logic.Inventory;
using System.Collections.Generic;

namespace Pewpew.Logic.Loot
{
    public class Drop
    {
        public Dictionary<ItemInfo,float> Chance { get; private set; }
        public Drop()
        {
            Chance = new Dictionary<ItemInfo, float>();
        }

        public void Add(ItemInfo itemInfo, float chance)
        {
            if (Chance.ContainsKey(itemInfo))
                Replace(itemInfo, chance);
            else
                Chance.Add(itemInfo, chance);
        }

        private void Replace(ItemInfo itemInfo, float chance)
        {
            Chance[itemInfo] = chance;
        }
    }
}
