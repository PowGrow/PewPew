using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    private Vector3 _rotation;

    private void Awake()
    {
        _rotation = new Vector3(0, 1, 0);
    }
    private void FixedUpdate()
    {
        transform.Rotate(_rotation, Speed * Time.fixedDeltaTime, Space.World);
    }
}
