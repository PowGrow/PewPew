using Pewpew.Logic.Loot;

namespace Pewpew.Infrastructure.States
{
    public struct LoadLevelPayload
    {
        public float BorderSize { get; private set; }
        public int AsteroidDensity { get; private set; }
        public LootTable LootTable { get; private set; }

        public LoadLevelPayload(float borderSize, int asteroidDensity, LootTable lootTable)
        {
            BorderSize = borderSize;
            AsteroidDensity = asteroidDensity;
            LootTable = lootTable;
        }
    }
}
