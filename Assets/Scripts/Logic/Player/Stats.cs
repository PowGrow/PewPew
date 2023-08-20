using UnityEngine;

namespace Pewpew.Player
{
    public class Stats : MonoBehaviour
    {
        [SerializeField]
        private Ship ShipInfo;

        public float Speed { get; private set; }
        public float Torque { get; private set; }
        public float Health { get; private set; }
        public float Armor { get; private set; }
        public float Shields { get; private set; }
        public float Damage { get; private set; }
        public WeaponType Weapon { get; private set; }
        public float RateOfFire { get; private set; }
        public float ReloadTime { get; private set; }
        public float AmmoStock { get; private set; }
        public float FireDistance { get; private set; }
        public float FireSpread { get; private set; }

        private void Awake()
        {
            SetInitialShipStats();
        }

        private void SetInitialShipStats()
        {
            Speed = ShipInfo.Speed;
            Torque = ShipInfo.Torque;
            Health = ShipInfo.Health;
            Armor = ShipInfo.Armor;
            Shields = ShipInfo.Shields;
            Damage = ShipInfo.Damage;
            Weapon = ShipInfo.Weapon;
            RateOfFire = ShipInfo.RateOfFire;
            ReloadTime = ShipInfo.ReloadTime;
            AmmoStock = ShipInfo.AmmoStock;
            FireDistance = ShipInfo.FireDistance;
            FireSpread = ShipInfo.FireSpread;
        }
    }
}
