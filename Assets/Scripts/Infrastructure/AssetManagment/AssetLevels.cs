using Pewpew.Logic.Map;
using System.Collections.Generic;

namespace Pewpew.Infrastructure.AssetManagment
{
    public static class AssetLevels
    {
        public const string GameLevelName = "Game";

        public const float SmallBorderSize = 0f;
        public const float MediumBorderSize = 1f;
        public const float LargeBorderSize = 1.5f;


        public const float SmallAsteroidSize = 1f;
        public const float MediumAsteroidSize = 3f;
        public const float LargeAsteroidSize = 7f;

        public const float SmallAsteroidPerlinMin = 93f;
        public const float MediumAsteroidPerlinMin = 95f;
        public const float LargeAsteroidPerlinMin = 99f;

        public static readonly Dictionary<AsteroidTypes,float> AsteroidSizes = new Dictionary<AsteroidTypes, float>
        {
            {AsteroidTypes.Small, SmallAsteroidPerlinMin },
            {AsteroidTypes.Medium, MediumAsteroidPerlinMin},
            {AsteroidTypes.Large, LargeAsteroidPerlinMin },
        };
    }
}
