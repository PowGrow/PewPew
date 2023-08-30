using Pewpew.Infrastructure.Services;
using Pewpew.Infrastructure.Services.Inventory;
using Pewpew.Logic.Asteroids;
using Pewpew.Logic.Inventory;
using System.Collections.Generic;

namespace Pewpew.Logic.Loot
{
    public class LootTable
    {
        public Dictionary<AsteroidTypes, Drop> Table { get; private set; }

        private Items _itemsInfo;

        private IItemsInfoService _itemsInfoService;
        public LootTable()
        {
            _itemsInfoService = AllServices.Container.Single<IItemsInfoService>();
            _itemsInfo = _itemsInfoService.Items;
            Table = new Dictionary<AsteroidTypes, Drop>();
        }

        public void Add(AsteroidTypes entity, Drop chancesToDrop)
        {
            if (Table.ContainsKey(entity))
                Replace(entity, chancesToDrop);
            else
                Table.Add(entity, chancesToDrop);
        }

        public void Add(AsteroidTypes entity, ItemInfo info, float chance)
        {
            if (Table.ContainsKey(entity))
            {
                var drop = Table[entity];
                drop.Add(info, chance);
            }
            else
            {
                Table.Add(entity, new Drop());
                Table[entity].Add(info, chance);
            }
        }

        public void Add(AsteroidTypes entity, int itemId, float chance)
        {
            if (Table.ContainsKey(entity))
            {
                var drop = Table[entity];
                drop.Add(_itemsInfo.GetItemInfo(itemId), chance);
            }
            else
            {
                Table.Add(entity, new Drop());
                Table[entity].Add(_itemsInfo.GetItemInfo(itemId), chance);
            }
        }

        private void Replace(AsteroidTypes entity, Drop chancesToDrop)
        {
            Table[entity] = chancesToDrop;
        }
    }
}
