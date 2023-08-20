using Pewpew.Infrastructure.Services;
using UnityEngine;

namespace Pewpew.Infrastructure.Factory
{
    public interface IBulletFactory: IService
    {
        public Bullet CreateBullet(Vector3 at, Quaternion faceTo);
        public void CreateBulletPool(string bulletPrefabPath, float Damage);
    }
}