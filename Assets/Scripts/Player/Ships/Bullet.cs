using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private string RangeLayerName = "BulletRange";
    public bool IsActive { get { return gameObject.activeSelf; } }

    private void Awake()
    {
        Set(active: false);
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
    private void OnTriggerExit(Collider other)
    {
            Set(active: false);
    }
    public void Set(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
