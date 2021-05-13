using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Health : MonoBehaviour, IDamagable
{
    public event DamageAction DamageEvent;
    public event DamageAction DeathEvent;

    [SerializeField] private float m_MaxHealth;
    [SerializeField] private float m_CurrentHealth;

    public float CurrentHealth => m_CurrentHealth;
    public float MaxHealth => m_MaxHealth;

    private void OnEnable()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    public void Damage(GameObject damager, float damage, Vector2 point, Vector2 direction)
    {
        m_CurrentHealth -= damage;

        DamageEvent?.Invoke(damager, damage, point, direction);

        if (m_CurrentHealth < 0f)
        {
            Kill(damager, damage, point, direction);
        }
    }

    private void Kill(GameObject damager, float damage, Vector2 point, Vector2 direction)
    {
        DeathEvent?.Invoke(damager, damage, point, direction);

        gameObject.SetActive(false);
    }
}
