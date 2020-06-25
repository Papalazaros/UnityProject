public interface IHealth
{
    float CurrentHealth { get; set; }
    float StartingHealth { get; set; }
    float MaxHealth { get; set; }
    float MinHealth { get; set; }
    float PassiveRegenRate { get; set; }
    float TickRate { get; set; }
    bool EnablePassiveRegen { get; set; }
    void TakeDamage(float damage, int applyOverDuration = 0);
    void Heal(float amount, int applyOverDuration = 0);
    void InhibitPassiveRegenRate(float duration);
}
