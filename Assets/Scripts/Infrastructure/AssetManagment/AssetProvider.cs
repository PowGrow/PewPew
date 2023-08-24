using UnityEngine;

namespace Pewpew.Infrastructure.AssetManagment
{
    public class AssetProvider : IAssetProvider
    {

        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public GameObject Instantiate(string path, Vector3 at, Quaternion faceTo)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, faceTo);
        }

        public GameObject Instantiate(string path, Vector3 at, Quaternion faceTo, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, faceTo,parent);
        }

        public Type Instantiate<Type>(string path, Vector3 at, Quaternion faceTo, Transform parent) where Type: Object
        {
            var type = Resources.Load<Type>(path);
            return Object.Instantiate(type, at, faceTo, parent);
        }

        public Type Instantiate<Type>(string path, Vector3 at) where Type : Object
        {
            var type = Resources.Load<Type>(path);
            return Object.Instantiate(type, at, Quaternion.identity);
        }

    }
}
