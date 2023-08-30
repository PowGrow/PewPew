using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Logic.Asteroids;
using Pewpew.Logic.Inventory;
using Pewpew.Logic.Map;
using PewPew.Infrastructure.AssetManagment;
using Unity.VisualScripting;
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

        public Type CreateAsteroid<Type>(Vector3 at, Quaternion rotation, AsteroidTypes type, AsteroidSizes size, Transform parent) where Type: Object
        {
            var asteroidSize = (int)size;
            var asteroid =  _assets.Instantiate<Type>(AssetPath.GetAsteroidPrefabPath(type), at, faceTo: rotation, parent);
            asteroid.GameObject().transform.localScale = new Vector3(asteroidSize, asteroidSize, asteroidSize);
            return asteroid;
        }

        public GameObject CreateAsteroidContainer()
        {
            return _assets.Instantiate(AssetPath.AsteroidContainerPrefabPath);
        }

        public GameObject CreateLoot(ItemInfo itemInfo, Vector3 at)
        {
            var loot = _assets.Instantiate<Loot>(AssetItems.ItemsPrefabPath + itemInfo.Name, at);
            loot.SetItem(new Item(itemInfo.Id, Random.Range(1, AssetLevels.LootDropQuantity)));
            return loot.gameObject;
        }

        public void CreateAsteroidParticles(Vector3 at)
        {
            var particles = _assets.Instantiate<ParticleSystem>(AssetPath.GetAsteroidDestroyParticlesPath(),at);
            particles.Play();
        }
    }
}
