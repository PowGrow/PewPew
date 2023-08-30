using Pewpew.Infrastructure.Services;
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
        (GameObject, AsteroidData) CreateAsteroid(Vector3 at, Quaternion rotation, AsteroidTypes type, AsteroidSizes size, Transform parent);
        Asteroids CreateAsteroidsBehaviour();
        GameObject CreateLoot(ItemInfo itemInfo, Vector3 at);
    }
}