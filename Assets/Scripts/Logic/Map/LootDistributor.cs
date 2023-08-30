using Pewpew.Infrastructure.Factory;
using Pewpew.Logic.Inventory;
using Pewpew.Logic.Loot;
using System.Collections.Generic;
using UnityEngine;

namespace Pewpew.Logic.Map
{
    public class LootDistributor
    {
        private IGameFactory _gameFactory;
        private LootTable _lootTable;
        private Items _items;

        public LootDistributor(IGameFactory gameFactory, LootTable lootTable,Items items, Asteroids asteroids)
        {
            _gameFactory = gameFactory;
            _lootTable = lootTable;
            _items = items;
            asteroids.OnAsteroidLootDroping += GetLoot;
        }

        private void GetLoot(AsteroidTypes entityType, Vector3 at)
        {
            var randomChance = Random.Range(0f, _lootTable.SummChance[entityType]);
            foreach (KeyValuePair<ItemInfo, float> itemInfoChance in _lootTable.Table[entityType].Chance)
            {
                if(randomChance <= itemInfoChance.Value)
                    _gameFactory.CreateLoot(_items.GetItemInfo(itemInfoChance.Key.Id), at);
            }
        }
    }
}
