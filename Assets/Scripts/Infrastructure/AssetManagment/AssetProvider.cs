using UnityEngine;
namespace Pewpew.Infrastructure.AssetManagment
{
    public class AssetProvider : IAssetProvider
    {

        public GameObject Instantiate(string path)
        {
            var playerPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(playerPrefab);
        }
        public GameObject Instantiate(string path, Vector3 at)
        {
            var playerPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(playerPrefab, at, Quaternion.identity);
        }

    }
}
