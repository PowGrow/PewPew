using System;
using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public event Action<Asteroid, string, Vector3> OnLootDroping; 

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
    private Camera _mainCam;
    private float _visibilityOffset = 3f;
    protected string _type = "Asteroid";

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
        OnLootDroping?.Invoke(this, _type, transform.position);
    }

    private void Awake()
    {
        Rotator = new AsteroidRotator(gameObject, UnityEngine.Random.Range(3f, 10f));
        Switch = new RendererSwitch(MeshRenderer,AsteroidCollider, ParentParticles, Rotator);
    }

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        Rotator.Execute();
    }

    private void FixedUpdate()
    {
        CheckVisibility();
    }

    //private void OnBecameVisible()
    //{
    //    Switch.OnBecameVisible();
    //}

    //private void OnBecameInvisible()
    //{
    //    Switch.OnBecameInvisible();
    //}

    private void CheckVisibility()
    {
        var objectPosition = _mainCam.WorldToScreenPoint(this.transform.position);
        if(objectPosition.x < 0 | objectPosition.y < 0 | objectPosition.x > _mainCam.pixelWidth | objectPosition.y > _mainCam.pixelHeight)
        {
            OnInvisible();
        }
        else
        {
            OnVisible();
        }
    }

    private void OnVisible()
    {
        Switch.OnBecameVisible();
    }

    private void OnInvisible()
    {
        Switch.OnBecameInvisible();
    }
}
