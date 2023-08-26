using UnityEngine;

public class RendererSwitch
{
    private MeshRenderer _renderer;
    private Collider _collider;
    private GameObject _particles;
    private AsteroidRotator _asteroidRotator;

    public RendererSwitch(MeshRenderer renderer, Collider collider, GameObject particles, AsteroidRotator asteroidRotator)
    {
        _renderer = renderer;
        _collider = collider;
        _particles = particles;
        _asteroidRotator = asteroidRotator;
    }

    public void OnBecameInvisible()
    {
        SetComponent(enable: false);
    }

    public void OnBecameVisible()
    {
        SetComponent(enable: true);

    }

    private void SetComponent(bool enable)
    {
        //_renderer.enabled = enable;
        _collider.enabled = enable;
        _particles.SetActive(enable);
        _asteroidRotator.IsActive = enable;
    }
}
