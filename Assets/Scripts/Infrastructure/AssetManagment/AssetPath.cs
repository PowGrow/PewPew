using Pewpew.Logic.Map;
using Pewpew.Player;
using System;
using System.Collections.Generic;

namespace Pewpew.Infrastructure.AssetManagment
{
    public static class AssetPath
    {
        public const string ItemsDataPath = "Data/Items/Items.json";
        public const string PlayerPrefabPath = "Prefabs/#TEST_PLAYER#";
        public const string BorderPrefabPath = "Prefabs/Border";
        public const string LobbyHudPrefabPath = "Prefabs/Lobby-HUD";

        public const string BulletPrefabPath = "Prefabs/Ammo/Bullet";
        public const string RocketPrefabPath = "Prefabs/Ammo/Rocket";
        public const string BulletContainerPrefabPath = "Prefabs/Ammo/BULLET-CONTAINER";


        public const string AsteroidPrefabPath = "Prefabs/Asteroids/";
        public const string AsteroidParticlesPath = "Prefabs/Asteroids/Particles/";
        public const string AsteroidDamageParticles = "DamageParticles";
        public const string AsteroidDestroyParticles = "DestroyParticles";
        public const string AsteroidDefaultPrefabPath = "Asteroid";
        public const string AsteroidCopperPrefabPath = "Asteroid_copper";
        public const string AsteroidIronPrefabPath = "Asteroid_iron";


        public const string AsteroidContainerPrefabPath = "Prefabs/Asteroids/ASTEROID-CONTAINER";

        private static readonly Dictionary<AsteroidTypes, string> _asteroidTypes = new Dictionary<AsteroidTypes, string>()
        {
            {AsteroidTypes.Empty, AsteroidDefaultPrefabPath },
            {AsteroidTypes.Copper, AsteroidCopperPrefabPath },
            {AsteroidTypes.Iron, AsteroidIronPrefabPath },
        };

        public static readonly Dictionary<WeaponType, string> WeaponAmmoPrefabPaths = new Dictionary<WeaponType, string>()
        {
            {WeaponType.MachineGun, BulletPrefabPath },
            {WeaponType.RocketLauncher, RocketPrefabPath },
            {WeaponType.Laser, null },
        };

        public static string GetAsteroidPrefabPath(AsteroidTypes type)
        {
            return AsteroidPrefabPath + _asteroidTypes[type];
        }

        public static string GetAsteroidDamageParticlesPath()
        {
            return AsteroidParticlesPath + AsteroidDamageParticles;
        }

        public static string GetAsteroidDestroyParticlesPath()
        {
            return AsteroidParticlesPath + AsteroidDestroyParticles;
        }
    }
}
