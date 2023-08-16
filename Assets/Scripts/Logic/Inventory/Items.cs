using Pewpew.Logic.Inventory;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Logic.Inventory
{
    public class Items
    {
        public HashSet<ItemInfo> ItemsInfo { get; private set; }

        public Items(List<ItemInfo> itemsInfo)
        {
            ItemsInfo = itemsInfo.ToHashSet();
        }

        public ItemInfo GetItemInfo(int itemId)
        {
            return ItemsInfo.Where(itemInfo => itemInfo.Id == itemId).First();
        }
    }
}
