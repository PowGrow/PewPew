using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Infrastructure.Services;
using Pewpew.Logic.Asteroids;
using Pewpew.Logic.Inventory;
using Pewpew.Logic.Map;
using UnityEngine;

namespace Pewpew.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        
        GameObject CreatePlayer(GameObject at);
        GameObject CreateLobbyHud();
        GameObject CreateGameBorder(float borderSize, GameObject at);
        Type CreateAsteroid<Type>(Vector3 at, Quaternion rotation, AsteroidTypes type, AsteroidSizes size, Transform parent) where Type : Object;
        GameObject CreateAsteroidContainer();
        GameObject CreateLoot(ItemInfo itemInfo, Vector3 at);
        void CreateAsteroidParticles(Vector3 at);
    }
}