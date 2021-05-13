using System;
using UnityEngine;

public interface IDamagable
{
    event DamageAction DamageEvent;
    event DamageAction DeathEvent;

    GameObject gameObject { get; }
    Transform transform { get; }

    float CurrentHealth { get; }
    float MaxHealth { get; }


    void Damage(GameObject damager, float damage, Vector2 point, Vector2 direction);
}

public delegate void DamageAction(GameObject damager, float damage, Vector2 point, Vector2 direction);