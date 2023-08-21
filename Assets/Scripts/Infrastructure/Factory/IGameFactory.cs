using Pewpew.Infrastructure.Services;
using Pewpew.Logic.Map;
using UnityEngine;

namespace Pewpew.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        GameObject CreatePlayer(GameObject at);
        GameObject CreateLobbyHud();
        GameObject CreateGameBorder(float borderSize, GameObject at);
        GameObject CreateAsteroid(Vector3 at, Quaternion rotation, AsteroidTypes type, Transform parent);
        GameObject CreateAsteroidContainer();
    }
}