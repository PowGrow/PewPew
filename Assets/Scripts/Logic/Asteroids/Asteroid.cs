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
    private ParticleSystem DestroyParticles;
    [SerializeField]
    private ParticleSystem DamageParticles;


    [SerializeField]
    [Range(0, 5)]
    private int Health;
    [SerializeField]
    private AsteroidTypes Type;

    public AsteroidRotator Rotator { get; private set; }
    public RendererSwitch Switch { get; private set; }


    [ContextMenu("TestDamage")]
    public void TestDamage()
    {
        GetDamage();
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
        DestroyParticles.Play();
        StartCoroutine(HideAsteroid(0.5f));
        DropLoot();
        StartCoroutine(DestoryAsteroid(1.5f));
    }

    private IEnumerator DestoryAsteroid(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private IEnumerator HideAsteroid(float delay)
    {
        yield return new WaitForSeconds(delay);
        MeshRenderer.enabled = false;
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
