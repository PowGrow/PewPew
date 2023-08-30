using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Infrastructure.Factory;
using Pewpew.Logic.Asteroids;
using System;
using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public event Action<Asteroid, AsteroidTypes, Vector3> OnLootDroping; 

    [SerializeField]
    private MeshRenderer MeshRenderer;
    [SerializeField]
    private Collider AsteroidCollider;
    [SerializeField]
    private GameObject ParentParticles;
    [SerializeField]
    private ParticleSystem DamageParticles;

    [SerializeField]
    private AsteroidTypes Type;

    public int Health { get; private set; }
    public AsteroidRotator Rotator { get; private set; }
    public RendererSwitch Switch { get; private set; }

    private IGameFactory _gameFactory;


    [ContextMenu("TestDamage")]
    public void TestDamage()
    {
        GetDamage();
    }

    public void SetHealth(int health)
    {
        Health = health;
    }

    public void SetGameFactory(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }

    public void GetDamage()
    {
        Health--;
        DamageParticles.Play();
        if (Health <= 0)
            Destruct();
    }

    private void Destruct()
    {
        AsteroidCollider.enabled = false;
        _gameFactory.CreateAsteroidParticles(gameObject.transform.position);
        DropLoot();
        Destroy(gameObject);
    }
    public void DropLoot()
    {
        OnLootDroping?.Invoke(this, Type, transform.position);
    }

    private void Awake()
    {
        Rotator = new AsteroidRotator(gameObject, UnityEngine.Random.Range(3f, 10f));
        Switch = new RendererSwitch(AsteroidCollider, ParentParticles, this);
    }

    private void Update()
    {
        Rotator.Execute();
    }

    private void OnBecameVisible()
    {
        Switch.OnBecameVisible();
    }

    private void OnBecameInvisible()
    {
        Switch.OnBecameInvisible();
    }
}
