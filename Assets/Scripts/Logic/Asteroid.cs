using System;
using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer MeshRenderer;
    [SerializeField]
    private ParticleSystem DestroyParticles;
    [SerializeField]
    private ParticleSystem DamageParticles;

    [SerializeField]
    [Range(0, 5)]
    private int Health;

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
        DestroyParticles.Play();
        StartCoroutine(HideAsteroid(0.5f));
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
}
