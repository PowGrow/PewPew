using Pewpew.Player;

public class Health
{
    public float MaximumHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float Armor { get; private set; }
    public float Shields { get; private set; }

    public Health(Stats stats)
    {
        MaximumHealth = stats.Health;
        CurrentHealth = MaximumHealth;
        Armor = stats.Armor;
        Shields = stats.Shields;
    }

    public void ApplyDamage(float damage)
    {
        var resultDamage = ApplyShieldDamage(damage);
        CurrentHealth -= resultDamage - (resultDamage / 100 * Armor);

        if (CurrentHealth <= 0)
            EmergencyExit();
    }

    private float ApplyShieldDamage(float damage)
    {
        if (Shields <= 0)
            return damage;

        if(damage > Shields)
        {
            damage -= Shields;
            Shields = 0;
            return damage;
        }

        Shields -= damage;
        return 0;
    }

    private void EmergencyExit()
    {
        //Return to lobby.. end run
    }
}
