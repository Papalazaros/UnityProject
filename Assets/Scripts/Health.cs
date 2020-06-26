using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IHealth
{
    public float PassiveRegenRate { get; set; }
    public float CurrentHealth { get; set; }
    public float StartingHealth { get; set; }
    public float MinHealth { get; set; }
    public float MaxHealth { get; set; }
    public float TickRate { get; set; }
    public bool EnablePassiveRegen { get; set; }

    [SerializeField]
    private Slider HealthBar;

    public void Heal(float amount, int applyOverDuration = 0)
    {
        StartCoroutine(TakeDamageOverTime(-amount, applyOverDuration));
    }

    public void InhibitPassiveRegenRate(float duration)
    {
        if (EnablePassiveRegen)
        {
            StartCoroutine(PausePassiveRegen(duration));
        }
    }

    public void TakeDamage(float damage, int applyOverDuration = 0)
    {
        StartCoroutine(TakeDamageOverTime(damage, applyOverDuration));
    }

    public IEnumerator PausePassiveRegen(float duration)
    {
        EnablePassiveRegen = false;
        float remainingDuration = duration;

        for (;;)
        {
            if (remainingDuration <= 0)
            {
                EnablePassiveRegen = true;
                yield break;
            }
            remainingDuration -= TickRate;
            yield return new WaitForSeconds(TickRate);
        }
    }

    public IEnumerator TakeDamageOverTime(float damage, int applyOverDuration = 0)
    {
        if (applyOverDuration == 0)
        {
            CurrentHealth -= damage;
            yield break;
        }

        float remainingDamage = damage;
        float damagePerTick = damage / (applyOverDuration / TickRate);

        while (remainingDamage > 0)
        {
            CurrentHealth -= damagePerTick;
            remainingDamage -= damagePerTick;
            yield return new WaitForSeconds(TickRate);
        }
    }

    private void Start()
    {
        CurrentHealth = StartingHealth;
    }

    private void Update()
    {
        if (EnablePassiveRegen) CurrentHealth += PassiveRegenRate * Time.deltaTime;
        CurrentHealth = Mathf.Clamp(CurrentHealth, MinHealth, MaxHealth);
        HealthBar.value = CurrentHealth;
    }
}
