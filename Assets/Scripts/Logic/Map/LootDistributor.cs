using Pewpew.Infrastructure.Factory;
using Pewpew.Logic.Asteroids;
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

        private List<Asteroid> _asteroids;
        public LootDistributor(IGameFactory gameFactory, LootTable lootTable,Items items, List<Asteroid> asteroids)
        {
            _gameFactory = gameFactory;
            _lootTable = lootTable;
            _items = items;
            _asteroids = asteroids;
            SubscribeOnLootDroping(asteroids);
        }
        private void SubscribeOnLootDroping(List<Asteroid> asteroids)
        {
            SubscribeOnAsteroidLootDroping(asteroids);
        }

        private void SubscribeOnAsteroidLootDroping(List<Asteroid> asteroids)
        {
            foreach(Asteroid asteroid in asteroids)
            {
                asteroid.OnLootDroping += GetLoot;
            }
        }

        private void GetLoot(Asteroid asteroid, AsteroidTypes entityType, Vector3 at)
        {
            try
            {
                foreach (KeyValuePair<ItemInfo,float> itemInfoChance in _lootTable.Table[entityType].Chance)
                {
                    var randomChance = Random.Range(0, 100f);
                    if (randomChance <= itemInfoChance.Value)
                        _gameFactory.CreateLoot(_items.GetItemInfo(itemInfoChance.Key.Id), at);
                }
                asteroid.OnLootDroping -= GetLoot;
            }
            catch(KeyNotFoundException)
            {

            }
        }
    }
}
