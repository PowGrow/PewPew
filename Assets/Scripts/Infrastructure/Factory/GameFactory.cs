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

        public GameObject CreateAsteroid(Vector3 at, Quaternion rotation, AsteroidTypes type, Transform parent)
        {
            return _assets.Instantiate(AssetPath.AsteroidPrefabPaths[type], at, faceTo: rotation, parent); ;
        }

        public GameObject CreateAsteroidContainer()
        {
            return _assets.Instantiate(AssetPath.AsteroidContainerPrefabPath);
        }
    }
}
