using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Infrastructure.Factory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pewpew.Logic.Map
{
    public partial class Generator
    {
        private Map _asteroidMap;

        private IGameFactory _gameFactory;
        private int _asteroidDensity;

        public Generator(IGameFactory gameFactory, int asteroidsDensity)
        {
            _gameFactory = gameFactory;
            _asteroidDensity = asteroidsDensity;
        }

        public void GenerateAsteroids(int mapRadius)
        {
            _asteroidMap = InstantiateAsteroidMap(mapRadius);
            for (int x = -1 * mapRadius; x < mapRadius; x += _asteroidDensity)
            {
                for (int y = -1 * mapRadius; y < mapRadius; y += _asteroidDensity)
                {
                    if (!IsCoordinateInRadius(x,y,mapRadius) || !IsCoordinateAvaliable(x,y)) //!!!
                        continue;

                    var pointAsteroidScale = UnityEngine.Random.Range(0, 100f);

                    foreach(KeyValuePair<AsteroidTypes,float> type in AssetLevels.AsteroidSizes)
                    {
                        if(pointAsteroidScale >= type.Value)
                        {
                            CreateAsteroid(at: (x, y), type.Key);
                            break;
                        }
                    }
                }
            }
        }

        private void CreateAsteroid((int x,int y) at, AsteroidTypes type)
        {
            var delta = Convert.ToInt32(type);
            if (IsPlaceAvaliable(at, delta))
            {
                PlaceAsteroid(at, delta);
                _gameFactory.CreateAsteroid(new Vector3(at.x, 0, at.y), Quaternion.identity, type);
            }

        }

        private bool IsPlaceAvaliable((int x, int y) at, int delta)
        {
            Func<int, int, bool> iterateFunc =
                (i, j) =>
                {
                    if (_asteroidMap[i, j] == 1)
                        return false;
                    return true;
                };
            return IterateMapPart(at, delta, iterateFunc);
        }

        private void PlaceAsteroid((int x, int y) at, int delta)
        {
            Func<int, int, bool> iterateFunc = 
                (i, j) =>
                {
                    _asteroidMap[i, j] = 1;
                    return false;
                };
            IterateMapPart(at, delta, iterateFunc);
        }
        private bool IterateMapPart((int x, int y) at, int delta, Func<int,int,bool> iterateFunc)
        {
            bool? isAllCellsAvaliable = true;
            for (int i = at.x - delta; i < at.x + delta; i++)
            {
                if (Mathf.Abs(i) >= _asteroidMap.Radius)
                    continue;
                for (int j = at.y - delta; j < at.y + delta; j++)
                {
                    if (Mathf.Abs(j) >= _asteroidMap.Radius)
                        continue;
                    isAllCellsAvaliable = iterateFunc?.Invoke(i,j);
                }
            }
            return (bool)isAllCellsAvaliable;
        }


        private bool IsCoordinateInRadius(float x, float y, float radius)
        {
            var hypotenuse = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            if (hypotenuse <= radius)
                return true;
            else
                return false;
        }

        private bool IsCoordinateAvaliable(int x, int y)
        {
            if (_asteroidMap[x, y] == 0)
                return true;
            else
                return false;
        }
        private Map InstantiateAsteroidMap(int mapRadius)
        {
            var asteroidMap = new Map(mapRadius);
            for (int x = -1 * mapRadius; x < mapRadius; x++)
            {
                for (int y = -1 * mapRadius; y < mapRadius; y++)
                {
                    if (IsCoordinateInRadius(x, y, mapRadius))
                        asteroidMap[x, y] = 0;
                    else
                        asteroidMap[x, y] = 1;
                }
            }
            return asteroidMap;
        }
    }
}
