using Pewpew.Infrastructure.Services;
using Pewpew.Infrastructure.Services.Inventory;
using Pewpew.Logic.Inventory;
using System.Collections.Generic;

namespace Pewpew.Logic.Loot
{
    public class LootTable
    {
        public Dictionary<AsteroidTypes, Drop> Table { get; private set; }
        public Dictionary<AsteroidTypes, float> SummChance { get; private set; }
        private Items _itemsInfo;

        private IItemsInfoService _itemsInfoService;
        public LootTable()
        {
            _itemsInfoService = AllServices.Container.Single<IItemsInfoService>();
            _itemsInfo = _itemsInfoService.Items;
            Table = new Dictionary<AsteroidTypes, Drop>();
            SummChance = new Dictionary<AsteroidTypes, float>();
        }

        public void Add(AsteroidTypes entity, ItemInfo info, float chance)
        {
            if (Table.ContainsKey(entity))
            {
                var drop = Table[entity];
                drop.Add(info, chance);
                SummChance[entity] += chance;
            }
            else
            {
                Table.Add(entity, new Drop());
                Table[entity].Add(info, chance);
                SummChance.Add(entity, chance);
            }
        }

        public void Add(AsteroidTypes entity, int itemId, float chance)
        {
            if (Table.ContainsKey(entity))
            {
                var drop = Table[entity];
                drop.Add(_itemsInfo.GetItemInfo(itemId), chance);
                SummChance[entity] += chance;
            }
            else
            {
                Table.Add(entity, new Drop());
                Table[entity].Add(_itemsInfo.GetItemInfo(itemId), chance);
                SummChance.Add(entity, chance);
            }
        }
    }
}
