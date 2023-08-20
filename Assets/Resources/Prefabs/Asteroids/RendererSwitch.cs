using UnityEngine;

public class RendererSwitch : MonoBehaviour
{
    [SerializeField]
    private MeshCollider MeshCollider;
    [SerializeField]
    private GameObject Particles;
    [SerializeField]
    private Asteroid Asteroid;

    private void Awake()
    {
        SetComponent(enable: false);
    }

    private void OnBecameInvisible()
    {
        SetComponent(enable: false);
    }

    private void OnBecameVisible()
    {
        SetComponent(enable: true);

    }

    private void SetComponent(bool enable)
    {
        MeshCollider.enabled = enable;
        Particles.SetActive(enable);
        Asteroid.enabled = enable;
    }
}
