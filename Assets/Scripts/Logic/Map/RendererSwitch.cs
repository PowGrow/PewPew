using UnityEngine;

public class RendererSwitch
{
    private Collider Collider;
    private GameObject Particles;
    private Asteroid Asteroid;

    public RendererSwitch(Collider collider, GameObject particles, Asteroid asteroid)
    {
        Collider = collider;
        Particles = particles;
        Asteroid = asteroid;
        SetComponent(enable: false);
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
        Collider.enabled = enable;
        Particles.SetActive(enable);
        Asteroid.Rotator.IsActive = enable;
    }
}
