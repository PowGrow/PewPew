﻿using Pewpew.Logic.Map;
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

        public const string AsteroidPrefabPath = "Prefabs/Asteroids/Asteroid";
        public const string AsteroidSmallPrefabPath = "Prefabs/Asteroids/Asteroid_small";
        public const string AsteroidMediumPrefabPath = "Prefabs/Asteroids/Asteroid_medium";
        public const string AsteroidLargePrefabPath = "Prefabs/Asteroids/Asteroid_large";
        public const string AsteroidContainerPrefabPath = "Prefabs/Asteroids/ASTEROID-CONTAINER";

        public static readonly Dictionary<AsteroidSizes, string> AsteroidPrefabPaths = new Dictionary<AsteroidSizes, string>()
        {
            {AsteroidSizes.Large, AsteroidLargePrefabPath},
            {AsteroidSizes.Medium, AsteroidMediumPrefabPath },
            {AsteroidSizes.Small, AsteroidSmallPrefabPath },
        };

        public static readonly Dictionary<WeaponType, string> WeaponAmmoPrefabPaths = new Dictionary<WeaponType, string>()
        {
            {WeaponType.MachineGun, BulletPrefabPath },
            {WeaponType.RocketLauncher, RocketPrefabPath },
            {WeaponType.Laser, null },
        };
    }
}
