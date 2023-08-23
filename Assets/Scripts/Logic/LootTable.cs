using Assets.Scripts.Logic.Inventory;
using Pewpew.Logic.Inventory;
using System.Collections.Generic;

namespace Pewpew.Logic.Loot
{
    public class LootTable<TEntity> where TEntity : class, IEntity
    {
        public Dictionary<TEntity, Drop> Table { get; private set; }

        private Items _items;
        public LootTable(Items items)
        {
            _items = items;
            Table = new Dictionary<TEntity, Drop>();
        }

        public void Add(TEntity entity, Drop chancesToDrop)
        {
            if (Table.ContainsKey(entity))
                Replace(entity, chancesToDrop);
            else
                Table.Add(entity, chancesToDrop);
        }

        public void Add(TEntity entity, ItemInfo info, float chance)
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

        public void Add(TEntity entity, int itemId, float chance)
        {
            if(Table.ContainsKey(entity))
            {
                var drop = Table[entity];
                drop.Add(_items.GetItemInfo(itemId), chance);
            }
            else
            {
                Table.Add(entity, new Drop());
                Table[entity].Add(_items.GetItemInfo(itemId), chance);
            }
        }

        private void Replace(TEntity entity, Drop chancesToDrop)
        {
            Table[entity] = chancesToDrop;
        }
    }
}
