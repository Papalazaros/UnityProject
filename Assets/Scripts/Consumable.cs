using System.Collections.Generic;

public sealed class Consumable : Item
{
    public IReadOnlyList<Effect> Effects { get; set; }

    public override void Consume()
    {
        IHealth health = Player.instance.Health;

        foreach (Effect effect in Effects)
        {
            if (effect.EffectType == EffectType.TakeDamage)
            {
                health.TakeDamage(effect.Amount, effect.Duration);
            }
        }
    }
}