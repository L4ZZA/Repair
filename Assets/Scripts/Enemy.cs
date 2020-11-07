using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    public float speed;
    public float timeBtwAttacks;
    public int damage;

    // between 0 and 100
    public int weaponPickupChance;
    public GameObject[] weaponsPickups;

    // between 0 and 100
    public int healthPickupChance;
    public GameObject healthPickup;

    protected Transform player;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            int randomNumber = Random.Range(0, 101);
            if(randomNumber < weaponPickupChance)
            {
                int randomIndex = Random.Range(0, weaponsPickups.Length);
                GameObject randPickup = weaponsPickups[randomIndex];
                Instantiate(randPickup, transform.position, transform.rotation);
            }

            int randomHealthNumber = Random.Range(0, 101);
            if(randomHealthNumber < healthPickupChance)
            {
                Instantiate(healthPickup, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}