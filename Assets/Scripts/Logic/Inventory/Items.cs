using PewPew.Infrastructure.AssetManagment;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pewpew.Logic.Inventory
{
    public class Items
    {
        public HashSet<ItemInfo> ItemsInfo { get; private set; }

        public Items(List<ItemInfo> itemsInfo)
        {
            ItemsInfo = itemsInfo.ToHashSet();

            //TEST_ITEMS
            ItemsInfo.Add(new ItemInfo(1, "Copper Ore", "Just copper ore", "Copper Ore", Resources.Load<GameObject>(AssetItems.ItemsPrefabPath + "Copper Ore"), true, 100));
            ItemsInfo.Add(new ItemInfo(2, "Iron Ore", "Just iron ore", "Iron Ore", Resources.Load<GameObject>(AssetItems.ItemsPrefabPath + "Iron Ore"), true, 100));
        }

        public Items()
        {
            ItemsInfo = new HashSet<ItemInfo>
            {
                new ItemInfo(1, "Copper Ore", "Just copper ore", "Copper Ore", Resources.Load<GameObject>(AssetItems.ItemsPrefabPath + "Copper Ore"), true, 100),
                new ItemInfo(2, "Iron Ore", "Just iron ore", "Iron Ore", Resources.Load<GameObject>(AssetItems.ItemsPrefabPath + "Iron Ore"), true, 100)
            };
        }

        public ItemInfo GetItemInfo(int itemId)
        {
            return ItemsInfo.Where(itemInfo => itemInfo.Id == itemId).First();
        }
    }
}
