
public interface IDoDamage
{
    int DamageAmount { get; }
    public string[] DamageableTags { get; }

    void DealDamage(int damageAmount, ITakeDamage damageable);

}
