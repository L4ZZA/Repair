using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EntityHealth : MonoBehaviour
{
    [Header("Type")]
    [SerializeField] private Entities.EntityType entityType;

    [Header("Health")]
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;

    private int previousHealth;

    [Header("Is this the player?")]
    [SerializeField] private bool isPlayer;

    [Header("Events")]
    [SerializeField] UnityEvent eventPlayerDied;
    [SerializeField] UnityEvent eventEnemyDied;
    [SerializeField] UnityEvent eventEnvironmentDied;
    [SerializeField] UnityEvent eventHealed;
    [SerializeField] UnityEvent eventDamaged;

    public static event Action<int> Action_PlayerHealthChanged = delegate { };
    public static event Action Action_PlayerDied= delegate { };


    /// <summary>
    /// External scripts change health via this function.
    /// </summary>
    public void ChangeHealth(int _damage)
    {
        // Store previous health before changing
        previousHealth = health;

        // Limit range of health
        health = Mathf.Clamp(health -= _damage, 0, maxHealth);

        CheckHealth(health);
    }


    /// <summary>
    /// Check what new health is.
    /// </summary>
    private void CheckHealth(int _health)
    {
        if (_health == 0)
        {
            // Check is player
            if (entityType == Entities.EntityType.player)
            {
                eventPlayerDied.Invoke();
            }

            // Check is environment object
            else if (entityType == Entities.EntityType.environment)
            {
                eventEnvironmentDied.Invoke();
            }

            // Otherwise, must be enemy
            else
            {
                eventEnemyDied.Invoke();
            }
        }

        else
        {
            // If health is less than before, it was damaged
            if (_health < previousHealth)
            {
                eventDamaged.Invoke();
            }

            // Otherwise, it was healed
            else
            {
                eventHealed.Invoke();
            }
        }
    }


    private void PlayerHealthChanged()
    {
        Action_PlayerHealthChanged.Invoke(health);
    }


    private void PlayerDied()
    {
        Action_PlayerDied.Invoke();
    }


}
