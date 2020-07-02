using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField, Range(0f, 100f)]
    private float _passiveRegenRate;
    [SerializeField, Range(0f, 100f)]
    private float _currentHealth;
    [SerializeField, Range(0f, 100f)]
    private float _minHealth;
    [SerializeField, Range(0f, 100f)]
    private float _maxHealth;
    [SerializeField, Range(0.25f, 1f)]
    private float _tickRate;
    [SerializeField]
    private bool _enablePassiveRegen;

    public float PassiveRegenRate { get => _passiveRegenRate; set => _passiveRegenRate = value; }
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public float MinHealth { get => _minHealth; set => _minHealth = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float TickRate { get => _tickRate; set => _tickRate = value; }
    public bool EnablePassiveRegen { get => _enablePassiveRegen; set => _enablePassiveRegen = value; }

    [SerializeField]
    private Slider _healthBar;

    public void Heal(float amount, int applyOverDuration = 0)
    {
        StartCoroutine(TakeDamageOverTime(-amount, applyOverDuration));
    }

    public void InhibitPassiveRegenRate(float duration)
    {
        if (_enablePassiveRegen)
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
        _enablePassiveRegen = false;
        float remainingDuration = duration;

        for (; ; )
        {
            if (remainingDuration <= 0)
            {
                _enablePassiveRegen = true;
                yield break;
            }
            remainingDuration -= _tickRate;
            yield return new WaitForSeconds(_tickRate);
        }
    }

    public IEnumerator TakeDamageOverTime(float damage, int applyOverDuration = 0)
    {
        if (applyOverDuration == 0)
        {
            _currentHealth -= damage;
            yield break;
        }

        float remainingDamage = damage;
        float damagePerTick = damage / (applyOverDuration / _tickRate);

        while (remainingDamage > 0)
        {
            _currentHealth -= damagePerTick;
            remainingDamage -= damagePerTick;
            yield return new WaitForSeconds(_tickRate);
        }
    }

    private void Update()
    {
        if (_currentHealth == _minHealth && gameObject.name != "Player") Destroy(gameObject);
        if (_enablePassiveRegen) _currentHealth += _passiveRegenRate * Time.deltaTime;
        _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);
        if (_healthBar != null) _healthBar.value = _currentHealth;
    }
}
