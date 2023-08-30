using Pewpew.Logic.Loot;
using System.Collections.Generic;

namespace Pewpew.Infrastructure.States
{
    public struct LoadLevelPayload
    {
        public float BorderSize { get; private set; }
        public int AsteroidDensity { get; private set; }
        public LootTable LootTable { get; private set; }

        public Dictionary<AsteroidTypes, float> MineralChances {get; private set;}

        public LoadLevelPayload(float borderSize, int asteroidDensity, LootTable lootTable, Dictionary<AsteroidTypes,float> mineralChances)
        {
            BorderSize = borderSize;
            AsteroidDensity = asteroidDensity;
            LootTable = lootTable;
            MineralChances = mineralChances;
        }
    }
}
