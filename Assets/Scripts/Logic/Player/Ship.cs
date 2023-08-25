using UnityEngine;

namespace Pewpew.Player
{
    [CreateAssetMenu(fileName = "ShipInfoObject", menuName = "ScriptableObjects/ShipInfoObject", order = 1)]
    public class Ship : ScriptableObject
    {
        public float Speed;
        public float Torque;
        public float Health;
        public float Armor;
        public float Shields;
        public float Damage;
        public WeaponType Weapon;
        public float RateOfFire;
        public float ReloadTime;
        public float AmmoStock;
        public float FireDistance;
        public float FireSpread;
        public int CargoSize;
    }
}
