using Pewpew.Logic.Map;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public event Action<GameObject, AsteroidTypes, Vector3> OnAsteroidLootDroping;
    public Dictionary<GameObject, AsteroidData> AsteroidsData { get; private set; }

    private Camera _mainCam;

    public Asteroids(Dictionary<GameObject, (AsteroidTypes type, AsteroidSizes size)> gameObjects)
    {
        AsteroidsData = CreateAsteroidsHashSet(gameObjects);
    }

    public void DropLoot(GameObject gameObject)
    {
        OnAsteroidLootDroping?.Invoke(gameObject, AsteroidsData[gameObject].Type, gameObject.transform.position);
        AsteroidsData.Remove(gameObject);
    }

    public void GetDamage(GameObject gameObject)
    {
        var asteroidData = AsteroidsData[gameObject];
        AsteroidsData[gameObject] = new AsteroidData(asteroidData.Rotator, 
                                                    asteroidData.Health - 1, 
                                                    asteroidData.Type);

        if (asteroidData.Health <= 0)
            Destruct(gameObject);
    }

    private void Destruct(GameObject gameObject)
    {
        DropLoot(gameObject);
    }

    private Dictionary<GameObject, AsteroidData> CreateAsteroidsHashSet(Dictionary<GameObject, (AsteroidTypes type,AsteroidSizes size)> gameObjects)
    {
        var healthMultiplier = 2;
        var index = 0;
        var list = new Dictionary<GameObject, AsteroidData>();
        foreach(KeyValuePair<GameObject, (AsteroidTypes type, AsteroidSizes size)> keyValuePair in gameObjects)
        {
            var rotator = new AsteroidRotator(keyValuePair.Key, UnityEngine.Random.Range(3f, 10f));
            var health = (int)keyValuePair.Value.size * healthMultiplier;
            list.Add(keyValuePair.Key,new AsteroidData(rotator, health, keyValuePair.Value.type));
            index++;
        }
        return list;
    }

    private void CheckVisibility(GameObject gameObject)
    {
        var objectPosition = _mainCam.WorldToScreenPoint(gameObject.transform.position);
        if (objectPosition.x < 0 | objectPosition.y < 0 | objectPosition.x > _mainCam.pixelWidth | objectPosition.y > _mainCam.pixelHeight)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        foreach (KeyValuePair<GameObject,AsteroidData> asteroidData in AsteroidsData)
        {
            asteroidData.Value.Rotator.Execute();
        }
    }
    private void FixedUpdate()
    {
        foreach (KeyValuePair<GameObject, AsteroidData> asteroidData in AsteroidsData)
        {
            CheckVisibility(asteroidData.Key);
        }
    }
}

public struct AsteroidData
{
    public AsteroidRotator Rotator { get; private set; }
    public int Health { get; set; }
    public AsteroidTypes Type { get; private set; }

    public AsteroidData(AsteroidRotator rotator, int health, AsteroidTypes type)
    {
        Rotator = rotator;
        Health = health;
        Type = type;
    }
}
