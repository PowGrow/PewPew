using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Infrastructure.Factory;
using Pewpew.Player;
using Pewpew.Services.Inputs;
using System.Collections.Generic;
using UnityEngine;

public class Guns
{
    private Ship _shipStats;
    private Transform _shootTarget;
    private List<Transform> _shootPositions;
    private SphereCollider _rangeCollider;

    private bool isActive = false;
    private float[] _fireCooldowns;

    private IInputService _inputService;
    private IBulletFactory _bulletFactory;

    public Guns(IInputService inputService, IBulletFactory bulletFactory, Ship shipStats, Transform shootTarget, List<Transform> shootPositions, SphereCollider rangeCollider)
    {
        _inputService = inputService;
        _bulletFactory = bulletFactory;
        _shipStats = shipStats;
        _shootTarget = shootTarget;
        _shootPositions = shootPositions;
        _rangeCollider = rangeCollider;
        Initialize(_shootPositions, _shootTarget, _rangeCollider, _shipStats);
    }

    private void CreateBulletPool(IBulletFactory bulletFactory, WeaponType weaponType, float Damage)
    {
        if (AssetPath.WeaponAmmoPrefabPaths[weaponType] != null)
            bulletFactory.CreateBulletPool(AssetPath.WeaponAmmoPrefabPaths[weaponType], Damage);
    }

    private void Initialize(List<Transform> shootPositions, Transform shootTarget, SphereCollider rangeCollider, Ship shipStats)
    {
        _fireCooldowns = new float[shootPositions.Count];
        shootTarget.position = new Vector3(shootTarget.position.x, shootTarget.position.y, shootTarget.position.z + shipStats.FireDistance);
        rangeCollider.radius = shootTarget.position.z;
        foreach (Transform shootPosition in shootPositions)
            shootPosition.LookAt(shootTarget);
        if (!isActive)
            isActive = true;
    }
    public void Start()
    {
        CreateBulletPool(_bulletFactory, _shipStats.Weapon, _shipStats.Damage);
    }

    public void Execute()
    {
        if (!isActive) 
            return;

        if (_inputService.IsAttackButtonDown())
            Fire();

        for (int i = 0; i < _fireCooldowns.Length; i++)
            _fireCooldowns[i] += Time.deltaTime;
    }

    private void Fire()
    {
        for(int i = 0; i < _fireCooldowns.Length; i++)
        {
            if (_fireCooldowns[i] >= _shipStats.RateOfFire)
            {
                Shoot(_shootPositions[i]);
                _fireCooldowns[i] = 0;
            }
        }
    }

    private void Shoot(Transform transform)
    {
        var rotation = transform.rotation;
        var fireSpreadAngle = Random.Range(-1 * (_shipStats.FireSpread / 2) , _shipStats.FireSpread / 2);
        rotation = Quaternion.Euler(rotation.eulerAngles.x,rotation.eulerAngles.y + fireSpreadAngle , rotation.eulerAngles.z);
        _bulletFactory.CreateBullet(at: transform.position, faceTo: rotation);
    }
}
