using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Infrastructure.Factory;
using Pewpew.Infrastructure.Services;
using Pewpew.Infrastructure.Services.Inventory;
using Pewpew.Logic.Inventory;
using Pewpew.Player;
using Pewpew.Services.Inputs;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Ship stats")]
        [SerializeField]
        private Ship initStats;
        [Header("Guns settings")]
        [SerializeField]
        private Transform ShootTarget;
        [SerializeField]
        private List<Transform> ShootPositions;
        [SerializeField]
        private SphereCollider RangeCollider;
    [Header("Movement settings")]
        [SerializeField]
        private Rigidbody ShipRigidbody;
        [SerializeField]
        private Transform ShipTransform;
        [SerializeField]
        private float Speed;
        [SerializeField]
        private float DeltaTorque;
    [Header("Particle settings")]
        [SerializeField]
        private List<TrailRenderer> EngineTrails;
        [SerializeField]
        private Light EngineLight;
        [SerializeField]
        private List<float> TrailsOnAngles = new List<float>() { 45f, 315f, 135f, 225f };

    public Stats Stats { get; private set; }
    public Health Health { get; private set; }
    public Guns Guns { get; private set; }
    public Inventory Cargo { get; private set; }
    public Movement Engine { get; private set; }
    public EngineParticles EngineParticles { get; private set; }

    private void Awake()
    {
        var inputService = AllServices.Container.Single<IInputService>();
        var bulletFactory = AllServices.Container.Single<IBulletFactory>();
        var itemsInfoService = AllServices.Container.Single<IItemsInfoService>();

        Stats = new Stats(initStats);
        Health = new Health(Stats);
        Guns = new Guns(inputService, bulletFactory, initStats, ShootTarget, ShootPositions, RangeCollider);
        Cargo = new Inventory(itemsInfoService.ItemsInfo , initStats.CargoSize);
        Engine = new Movement(Stats, ShipRigidbody, ShipTransform, DeltaTorque);
        EngineParticles = new EngineParticles(inputService, TrailsOnAngles, EngineTrails, EngineLight, ShipTransform);

    }

    private void Start()
    {
        Guns.Start();
    }

    private void Update()
    {
        Guns.Execute();
    }

    private void FixedUpdate()
    {
        Engine.Execute();
        EngineParticles.Execute();
    }

}
