using UnityEngine;

public class AsteroidRotator
{
    public bool IsActive { get; set; } = true;

    private GameObject _asteroid;
    private float _speed;
    private Vector3[] _axis = new Vector3[3] 
    {
        new Vector3(1,0,0),
        new Vector3(0,1,0),
        new Vector3(0,0,1),
    };
    private int _selectedIndex = 0;
    public AsteroidRotator(GameObject asteroid, float speed)
    {
        _asteroid = asteroid;
        _speed = speed;
        _selectedIndex = Random.Range(0, 3);
    }

    public void Execute()
    {
        if(IsActive)
        _asteroid.transform.Rotate(_axis[_selectedIndex], _speed * Time.deltaTime);
    }
}
