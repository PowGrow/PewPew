using System;
using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public event Action<Asteroid, string, Vector3> OnLootDroping; 

    [SerializeField]
    private MeshRenderer MeshRenderer;
    [SerializeField]
    private Collider asteroidCollider;
    [SerializeField]
    private ParticleSystem DestroyParticles;
    [SerializeField]
    private ParticleSystem DamageParticles;

    [SerializeField]
    [Range(0, 5)]
    private int Health;
    protected string _type = "Asteroid";

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
        asteroidCollider.enabled = false;
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
        OnLootDroping?.Invoke(this, _type, transform.position);
    }
}
