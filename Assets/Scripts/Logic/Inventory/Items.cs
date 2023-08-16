using Pewpew.Logic.Inventory;
using PewPew.Infrastructure.AssetManagment;
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

            //TEST_ITEMS
            ItemsInfo.Add(new ItemInfo(1, "Copper Ore", "Just copper ore", AssetItems.CopperOre, true, 100));
            ItemsInfo.Add(new ItemInfo(2, "Iron Ore", "Just iron ore", AssetItems.IronOre, true, 100));
        }

        public ItemInfo GetItemInfo(int itemId)
        {
            return ItemsInfo.Where(itemInfo => itemInfo.Id == itemId).First();
        }
    }
}
