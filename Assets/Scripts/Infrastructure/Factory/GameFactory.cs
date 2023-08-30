using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Logic.Inventory;
using Pewpew.Logic.Map;
using PewPew.Infrastructure.AssetManagment;
using System.Collections.Generic;
using UnityEngine;

namespace Pewpew.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        

        private IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreatePlayer(GameObject at) =>
            _assets.Instantiate(AssetPath.PlayerPrefabPath, at: at.transform.position);

        public GameObject CreateLobbyHud() =>
            _assets.Instantiate(AssetPath.LobbyHudPrefabPath);

        public GameObject CreateGameBorder(float borderSize, GameObject at)
        {
            var border = _assets.Instantiate(AssetPath.BorderPrefabPath, at: at.transform.position);
            border.transform.localScale += new Vector3(borderSize, borderSize, borderSize);
            return border;
        }

        public (GameObject,AsteroidData) CreateAsteroid(Vector3 at, Quaternion rotation, AsteroidTypes type, AsteroidSizes size, Transform parent)
        {
            var asteroidSize = (int)size;
            var health = asteroidSize;
            var gameObject = _assets.Instantiate(AssetPath.GetAsteroidPrefabPath(type), at, faceTo: rotation, parent);
            gameObject.transform.localScale = new Vector3(asteroidSize, asteroidSize, asteroidSize);
            var damageParticles = _assets.Instantiate<ParticleSystem>(AssetPath.GetAsteroidDamageParticlesPath(), at, Quaternion.identity, gameObject.transform);
            var destroyParticles = _assets.Instantiate<ParticleSystem>(AssetPath.GetAsteroidDestroyParticlesPath(), at, Quaternion.identity, gameObject.transform);
            var rotator = new AsteroidRotator(gameObject, Random.Range(3f, 10f));
            return (gameObject,new AsteroidData(rotator, damageParticles, destroyParticles, health, type));
        }

        public Asteroids CreateAsteroidsBehaviour()
        {
            return _assets.Instantiate<Asteroids>(AssetPath.AsteroidContainerPrefabPath, Vector3.zero);
        }

        public GameObject CreateLoot(ItemInfo itemInfo, Vector3 at)
        {
            var loot = _assets.Instantiate<Loot>(AssetItems.ItemsPrefabPath + itemInfo.Name, at);
            loot.SetItem(new Item(itemInfo.Id, Random.Range(1, AssetLevels.LootDropQuantity)));
            return loot.gameObject;
        }
    }
}
