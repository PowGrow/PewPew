using Pewpew.Logic.Map;
using Pewpew.Player;
using System.Collections.Generic;

namespace Pewpew.Infrastructure.AssetManagment
{
    public static class AssetPath
    {
        public const string PlayerPrefabPath = "Prefabs/#TEST_PLAYER#";
        public const string BorderPrefabPath = "Prefabs/Border";
        public const string LobbyHudPrefabPath = "Prefabs/Lobby-HUD";
        public const string BulletPrefabPath = "Prefabs/Bullet";
        public const string RocketPrefabPath = "Prefabs/Rocket";

        public const string AsteroidPrefabPath = "Prefabs/Asteroids/Asteroid";
        public const string AsteroidSmallPrefabPath = "Prefabs/Asteroids/Asteroid_small Variant";
        public const string AsteroidMediumPrefabPath = "Prefabs/Asteroids/Asteroid_medium Variant";
        public const string AsteroidLargePrefabPath = "Prefabs/Asteroids/Asteroid_large Variant";
        public const string AsteroidContainerPrefabPath = "Prefabs/Asteroids/ASTEROID-CONTAINER";

        public static readonly Dictionary<AsteroidTypes, string> AsteroidPrefabPaths = new Dictionary<AsteroidTypes, string>()
        {
            {AsteroidTypes.Large, AsteroidLargePrefabPath},
            {AsteroidTypes.Medium, AsteroidMediumPrefabPath },
            {AsteroidTypes.Small, AsteroidSmallPrefabPath },
        };

        public static readonly Dictionary<WeaponType, string> WeaponAmmoPrefabPaths = new Dictionary<WeaponType, string>()
        {
            {WeaponType.MachineGun, BulletPrefabPath },
            {WeaponType.RocketLauncher, RocketPrefabPath },
            {WeaponType.Laser, null },
        };
    }
}
