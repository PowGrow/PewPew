﻿using Pewpew.Infrastructure.Services;
using UnityEngine;

namespace Pewpew.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        GameObject CreatePlayer(GameObject at);
        GameObject CreateLobbyHud();
        GameObject CreateGameBorder(float borderSize, GameObject at);
    }
}