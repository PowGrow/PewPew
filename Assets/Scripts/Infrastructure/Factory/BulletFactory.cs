using Pewpew.Infrastructure.AssetManagment;
using System.Collections.Generic;
using UnityEngine;

namespace Pewpew.Infrastructure.Factory
{
    public class BulletFactory : IBulletFactory
    {
        private IAssetProvider _assets;
        private List<Bullet> _bulletsPool = new List<Bullet>();

        private float _poolSize;

        public BulletFactory(IAssetProvider assets, float bulletPoolSize)
        {
            _assets = assets;
            _poolSize = bulletPoolSize;
        }

        public Bullet CreateBullet(Vector3 at, Quaternion faceTo)
        {
            foreach (Bullet bullet in _bulletsPool)
            {
                if (!bullet.IsActive)
                {
                    bullet.transform.position = at;
                    bullet.transform.rotation = faceTo;
                    bullet.Set(active: true);
                    return bullet;
                }
            }
            return null;
        }

        public void CreateBulletPool(string bulletPrefabPath, float Damage)
        {
            if( _bulletsPool.Count == 0 )
            {
                var bulletContainer = _assets.Instantiate(AssetPath.BulletContainerPrefabPath);
                for (int i = 0; i < _poolSize; i++)
                {
                    var bullet = InstantiateBullet(bulletPrefabPath, bulletContainer.transform);
                    bullet.Damage = Damage;
                    _bulletsPool.Add(bullet);
                }
            }
        }

        private Bullet InstantiateBullet(string bulletPrefabPath, Transform parent)
        {
            var at = new Vector3(0, -100, 0);
            Bullet bullet = _assets.Instantiate(bulletPrefabPath, at, Quaternion.identity, parent).GetComponent<Bullet>();
            return bullet;
        }
    }
}
