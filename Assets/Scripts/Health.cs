using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IHealth
{
    [Range(0f, 100f)]
    public float passiveRegenRate;
    [Range(0f, 100f)]
    public float startingHealth;
    [Range(0f, 100f)]
    public float minHealth;
    [Range(0f, 100f)]
    public float maxHealth;
    [Range(0.25f, 1.0f)]
    public float tickRate;
    public bool enablePassiveRegen;

    public float PassiveRegenRate { get => passiveRegenRate; set => passiveRegenRate = value; }
    public float CurrentHealth { get; set; }
    public float StartingHealth { get => startingHealth; set => startingHealth = value; }
    public float MinHealth { get => minHealth; set => minHealth = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float TickRate { get => tickRate; set => tickRate = value; }
    public bool EnablePassiveRegen { get => enablePassiveRegen; set => enablePassiveRegen = value; }

    public Slider HealthBar;

    public void Heal(float amount, int applyOverDuration = 0)
    {
        StartCoroutine(TakeDamageOverTime(-amount, applyOverDuration));
    }

    public void InhibitPassiveRegenRate(float duration)
    {
        if (enablePassiveRegen)
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
        enablePassiveRegen = false;
        float remainingDuration = duration;

        for (;;)
        {
            if (remainingDuration <= 0)
            {
                enablePassiveRegen = true;
                yield break;
            }
            remainingDuration -= tickRate;
            yield return new WaitForSeconds(tickRate);
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
        float damagePerTick = damage / (applyOverDuration / tickRate);

        while (remainingDamage > 0)
        {
            CurrentHealth -= damagePerTick;
            remainingDamage -= damagePerTick;
            yield return new WaitForSeconds(tickRate);
        }
    }

    private void Start()
    {
        CurrentHealth = startingHealth;
    }

    private void Update()
    {
        if (enablePassiveRegen) CurrentHealth += passiveRegenRate * Time.deltaTime;
        CurrentHealth = Mathf.Clamp(CurrentHealth, minHealth, maxHealth);
        HealthBar.value = CurrentHealth;
    }
}
