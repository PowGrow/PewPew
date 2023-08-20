using Pewpew.Infrastructure.Factory;
using Pewpew.Infrastructure.Services;
using Pewpew.Player;
using Pewpew.Services.Inputs;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    [SerializeField]
    private Transform ShootTarget;
    [SerializeField]
    private List<Transform> ShootPositions;
    [SerializeField]
    private SphereCollider RangeCollider;
    [SerializeField]
    private Stats ShipInfo;

    private bool isActive = false;
    private IInputService _inputService;
    private IBulletFactory _bulletFactory;
    private float[] _fireCooldowns;


    private void Awake()
    {
        _inputService = AllServices.Container.Single<IInputService>();
        _bulletFactory = AllServices.Container.Single<IBulletFactory>();
        _fireCooldowns = new float[ShootPositions.Count];
    }
    public void Initialize()
    {
        ShootTarget.position = new Vector3(ShootTarget.position.x, ShootTarget.position.y, ShootTarget.position.z + ShipInfo.FireDistance);
        RangeCollider.radius = ShootTarget.position.z;
        foreach (Transform shootPosition in ShootPositions)
            shootPosition.LookAt(ShootTarget);
        if (!isActive)
            isActive = true;
    }

    public void Update()
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
            if (_fireCooldowns[i] >= ShipInfo.RateOfFire)
            {
                Shoot(ShootPositions[i]);
                _fireCooldowns[i] = 0;
            }
        }
    }

    private void Shoot(Transform transform)
    {
        var rotation = transform.rotation;
        var fireSpreadAngle = Random.Range(-1 * (ShipInfo.FireSpread / 2) , ShipInfo.FireSpread / 2);
        rotation = Quaternion.Euler(rotation.eulerAngles.x,rotation.eulerAngles.y + fireSpreadAngle , rotation.eulerAngles.z);
        _bulletFactory.CreateBullet(at: transform.position, faceTo: rotation);
    }
}
