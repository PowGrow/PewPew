namespace Pewpew.Player
{
    public class Stats
    {
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
        public int CargoSize { get; private set; }

        public Stats(Ship shipStats)
        {
            SetInitialShipStats(shipStats);
        }

        private void SetInitialShipStats(Ship shipStats)
        {
            Speed = shipStats.Speed;
            Torque = shipStats.Torque;
            Health = shipStats.Health;
            Armor = shipStats.Armor;
            Shields = shipStats.Shields;
            Damage = shipStats.Damage;
            Weapon = shipStats.Weapon;
            RateOfFire = shipStats.RateOfFire;
            ReloadTime = shipStats.ReloadTime;
            AmmoStock = shipStats.AmmoStock;
            FireDistance = shipStats.FireDistance;
            FireSpread = shipStats.FireSpread;
            CargoSize = shipStats.CargoSize;
        }
    }
}
