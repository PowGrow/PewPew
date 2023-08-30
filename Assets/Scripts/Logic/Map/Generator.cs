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
        private Asteroids _asteroids;
        private Dictionary<AsteroidTypes,float> _mineralChances;

        public Generator(IGameFactory gameFactory, Dictionary<AsteroidTypes, float> mineralChances, int asteroidsDensity)
        {
            _gameFactory = gameFactory;
            _asteroidDensity = asteroidsDensity;
            _mineralChances = mineralChances;
        }

        public Asteroids GenerateAsteroids(int mapRadius)
        {
            _asteroids = _gameFactory.CreateAsteroidsBehaviour();
            _asteroidMap = InstantiateAsteroidMap(mapRadius);
            for (int x = -1 * mapRadius; x < mapRadius; x += _asteroidDensity)
            {
                for (int y = -1 * mapRadius; y < mapRadius; y += _asteroidDensity)
                {
                    if (!IsCoordinateInRadius(x, y, mapRadius) || !IsCoordinateAvaliable(x, y))
                        continue;

                    AsteroidTypes asteroidType = GetAsteroidType(_mineralChances);

                    var randomAsteroidSize = UnityEngine.Random.Range(0, 100f);
                    foreach (KeyValuePair<AsteroidSizes, float> size in AssetLevels.AsteroidSizes)
                    {
                        var delta = Convert.ToInt32(asteroidType) / 2;
                        if (randomAsteroidSize >= size.Value && IsPlaceAvaliable(at: (x, y), delta))
                        {
                            (GameObject gameObject, AsteroidData data) asteroid = CreateAsteroid(at: (x, y), asteroidType, size.Key, _asteroids.transform, delta);
                            _asteroids.AsteroidsData.Add(asteroid.gameObject, asteroid.data);
                            break;
                        }
                    }
                }
            }
            _asteroids.SetActive(true);
            return _asteroids;
        }

        private AsteroidTypes GetAsteroidType(Dictionary<AsteroidTypes, float> chances)
        {
            AsteroidTypes asteroidType = AsteroidTypes.Empty;
            var currentChance = 0f;
            var randomChance = UnityEngine.Random.Range(0, 100f);
            foreach (KeyValuePair<AsteroidTypes, float> type in chances)
            {
                if (randomChance < type.Value + currentChance && randomChance >= currentChance)
                {
                    asteroidType = type.Key;
                    break;
                }
                currentChance += type.Value;
            }

            return asteroidType;
        }

        private (GameObject, AsteroidData) CreateAsteroid((int x,int y) at, AsteroidTypes type, AsteroidSizes size, Transform parent, int delta)
        {
            PlaceAsteroid(at, delta);
            var randomRotation = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0, 359), UnityEngine.Random.Range(0, 359), UnityEngine.Random.Range(0, 359)));
            return _gameFactory.CreateAsteroid(new Vector3(at.x, 0, at.y), randomRotation, type, size, parent);
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
