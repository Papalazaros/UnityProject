﻿public interface IDamageable
{
    float Health { get; set; }
    float MaxHealth { get; set; }
    void TakeDamage(float damage);
}
