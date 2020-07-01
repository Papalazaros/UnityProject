using System.Collections.Generic;

public class Consumable : Item
{
    public static ItemAction[] DefaultItemActions = new ItemAction[]
    {
        ItemAction.Combine,
        ItemAction.Drop,
        ItemAction.Use
    };

    public IReadOnlyList<Effect> Effects { get; set; }

    public override bool Use()
    {
        IHealth health = Player.instance.Health;
        List<Effect> effectsApplied = new List<Effect>();

        foreach (Effect effect in Effects)
        {
            if (effect.EffectType == EffectType.TakeDamage)
            {
                if (effect.Amount <= 0 && health.CurrentHealth == health.MaxHealth) continue;
                health.TakeDamage(effect.Amount, effect.Duration);
                effectsApplied.Add(effect);
            }
        }

        return effectsApplied.Count > 0;
    }
}