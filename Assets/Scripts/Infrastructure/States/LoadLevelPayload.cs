using Pewpew.Logic.Inventory;
using Pewpew.Logic.Loot;
using System;

namespace Pewpew.Infrastructure.States
{
    public struct LoadLevelPayload
    {
        public float BorderSize { get; private set; }
        public int AsteroidDensity { get; private set; }
        public LootTable LootTable { get; private set; }
        public Items Items { get; private set; }

        public LoadLevelPayload(float borderSize, int asteroidDensity, LootTable lootTable, Items items)
        {
            BorderSize = borderSize;
            AsteroidDensity = asteroidDensity;
            LootTable = lootTable;
            Items = items;
        }
    }
}
