using System;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public event Action<AsteroidTypes, Vector3> OnAsteroidLootDroping;
    public Dictionary<GameObject, AsteroidData> AsteroidsData { get; private set; } = new Dictionary<GameObject, AsteroidData>();

    private Camera _mainCam;
    private bool _isActive = false;

    public void SetActive(bool state)
    {
        _isActive = state;
    }
    public void DropLoot(GameObject gameObject)
    {
        OnAsteroidLootDroping?.Invoke(AsteroidsData[gameObject].Type, gameObject.transform.position);
        AsteroidsData.Remove(gameObject);
    }

    public void GetDamage(GameObject gameObject)
    {
        var asteroidData = AsteroidsData[gameObject];
        AsteroidsData[gameObject] = new AsteroidData(asteroidData.Rotator,
                                                    asteroidData.DamageParticles,
                                                    asteroidData.DestroyParticles,
                                                    asteroidData.Health - 1,
                                                    asteroidData.Type);

        if (asteroidData.Health <= 0)
            Destruct(gameObject);
    }

    private void Destruct(GameObject gameObject)
    {
        DropLoot(gameObject);
    }

    private void CheckVisibility(KeyValuePair<GameObject, AsteroidData> asteroidData)
    {
        var objectPosition = _mainCam.WorldToScreenPoint(asteroidData.Key.transform.position);
        if (objectPosition.x < 0 | objectPosition.y < 0 | objectPosition.x > _mainCam.pixelWidth | objectPosition.y > _mainCam.pixelHeight)
        {
            asteroidData.Key.SetActive(false);
            asteroidData.Value.Rotator.IsActive = false;
        }
        else
        {
            asteroidData.Key.SetActive(true);
            asteroidData.Value.Rotator.IsActive = true;
        }
    }

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        if (!_isActive)
            return;
        foreach (KeyValuePair<GameObject,AsteroidData> asteroidData in AsteroidsData)
        {
            asteroidData.Value.Rotator.Execute();
        }
    }
    private void FixedUpdate()
    {
        if (!_isActive)
            return;
        foreach (KeyValuePair<GameObject, AsteroidData> asteroidData in AsteroidsData)
        {
            CheckVisibility(asteroidData);
        }
    }
}

public struct AsteroidData
{
    public AsteroidRotator Rotator { get; private set; }
    public ParticleSystem DamageParticles { get; private set; }
    public ParticleSystem DestroyParticles { get; private set; }
    public int Health { get; set; }
    public AsteroidTypes Type { get; private set; }

    public AsteroidData(AsteroidRotator rotator, ParticleSystem damageParticles, ParticleSystem destroyParticles, int health, AsteroidTypes type)
    {
        Rotator = rotator;
        DamageParticles = damageParticles;
        DestroyParticles = destroyParticles;
        Health = health;
        Type = type;
    }
}
