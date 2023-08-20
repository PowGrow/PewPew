using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Logic.Map;
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

        public GameObject CreateAsteroid(Vector3 at, Quaternion rotation, AsteroidTypes type)
        {
            GameObject asteroid;
            switch (type)
            {
                case AsteroidTypes.Small:
                    asteroid = _assets.Instantiate(AssetPath.AsteroidSmallPrefabPath, at, faceTo: rotation);
                    break;
                case AsteroidTypes.Medium:
                    asteroid = _assets.Instantiate(AssetPath.AsteroidMediumPrefabPath, at, faceTo: rotation);
                    break;
                case AsteroidTypes.Large:
                    asteroid = _assets.Instantiate(AssetPath.AsteroidLargePrefabPath, at, faceTo: rotation);
                    break;
                default:
                    asteroid = _assets.Instantiate(AssetPath.AsteroidSmallPrefabPath, at, faceTo: rotation);
                    break;
            }
            return asteroid;
        }
    }
}
