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
    private int maxHealth;

    [Header("Damage upon impact")]
    [SerializeField] private int damage;

    [Header("Destroy layer if touched")]
    [SerializeField] private List<LayerMask> layersCanAffect = new List<LayerMask>();

    [Header("Score (if destroyed)")]
    [SerializeField] private int score = 0;



    private int previousHealth;

    [Header("Events")]
    [SerializeField] UnityEvent eventPlayerDied;
    [SerializeField] UnityEvent eventEnemyDied;
    [SerializeField] UnityEvent eventEnvironmentDied;
    [SerializeField] UnityEvent eventHealed;
    [SerializeField] UnityEvent eventDamaged;

    [SerializeField] UnityEventDoubleInt eventEjectHealth;

    public static event Action<int> Action_PlayerHealthChanged = delegate { };
    public static event Action Action_PlayerDied= delegate { };

    public static event Action<int> Action_EnemyDied = delegate { };
    public static event Action Action_EnvironmentDied = delegate { };


    private void Awake()
    {
        maxHealth = health;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (layersCanAffect.Count > 0)
                {
            GameObject gameObj = collision.gameObject;

            EntityHealth entityHealth = gameObj.GetComponent<EntityHealth>();

            if (entityHealth != null)
            {
                foreach (var layer in layersCanAffect)
                {
                    if ((layer & 1 << gameObj.layer) == 1 << gameObj.layer)
                    {
                        DealDamage(entityHealth, damage);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }




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
                PlayerDied();
            }

            // Check is environment object
            else if (entityType == Entities.EntityType.environment)
            {
                EnvironmentDied();
            }

            // Otherwise, must be enemy
            else
            {
                EnemyDied();
                
            }

            
        }

        else
        {
            // If health is less than before, it was damaged
            if (_health < previousHealth)
            {
                Debug.Log("Damaged");
                eventDamaged.Invoke();
            }

            // Otherwise, it was healed
            else
            {
                Debug.Log("Healed");
                eventHealed.Invoke();
            }

            eventEjectHealth.Invoke(health, maxHealth);
        }
    }
    private void DealDamage(EntityHealth _entityHealth, int _damage)
    {
        _entityHealth.ChangeHealth(_damage);
    }



    private void PlayerHealthChanged()
    {
        Action_PlayerHealthChanged.Invoke(health);
    }


    private void PlayerDied()
    {
        Destroy(this.gameObject);
        Action_PlayerDied.Invoke();
    }


    private void EnvironmentDied()
    {
        eventEnvironmentDied.Invoke();
        Action_EnvironmentDied.Invoke();
    }

    private void EnemyDied()
    {
        Destroy(this.gameObject);
        eventEnemyDied.Invoke();
        Action_EnemyDied.Invoke(score);
    }

}



[System.Serializable]
public class UnityEventDoubleInt : UnityEvent<int, int>{ }
