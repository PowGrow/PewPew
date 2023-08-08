using Pewpew.Infrastructure.AssetManagment;
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
    }
}
