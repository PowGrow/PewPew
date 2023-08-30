using Pewpew.Logic.Map;
using System.Collections.Generic;

namespace Pewpew.Infrastructure.AssetManagment
{
    public static class AssetLevels
    {
        public const string GameLevelName = "Game";

        public const int   LootDropQuantity = 10;

        public const float BorderSizeСoefficient = 188.5f;

        public const float SmallBorderSize = 0f;
        public const float MediumBorderSize = 1f;
        public const float LargeBorderSize = 1.5f;


        public const float SmallAsteroidSize = 1f;
        public const float MediumAsteroidSize = 3f;
        public const float LargeAsteroidSize = 7f;

        public const float SmallAsteroidChanceMin = 93f;
        public const float MediumAsteroidChanceMin = 95f;
        public const float LargeAsteroidChanceMin = 99f;

        public static readonly Dictionary<AsteroidSizes,float> AsteroidSizes = new Dictionary<AsteroidSizes, float>
        {
            { Logic.Map.AsteroidSizes.Large, LargeAsteroidChanceMin },
            { Logic.Map.AsteroidSizes.Medium, MediumAsteroidChanceMin},
            { Logic.Map.AsteroidSizes.Small, SmallAsteroidChanceMin },
        };
    }
}
