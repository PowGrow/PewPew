using PewPew.Infrastructure.AssetManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pewpew.Logic.Inventory
{
    [Serializable]
    public class Items
    {
        [field:SerializeField]
        public HashSet<ItemInfo> List { get; private set; }

        public Items(List<ItemInfo> itemsInfo)
        {
            List = itemsInfo.ToHashSet();
        }

        public ItemInfo GetItemInfo(int itemId)
        {
            try
            {
                return List.Where(itemInfo => itemInfo.Id == itemId).First();
            }
            catch
            {
                return null;
            }
        }

        public List<ItemInfo> GetItemsList()
        {
            return List.ToList();
        }
    }
}
