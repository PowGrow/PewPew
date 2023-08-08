﻿using Pewpew.Infrastructure.Services;
using UnityEngine;

namespace Pewpew.Infrastructure.AssetManagment
{
    public interface IAssetProvider: IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}