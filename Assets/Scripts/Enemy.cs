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
    public int pickupChance;
    public GameObject[] pickups;

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
            if(randomNumber < pickupChance)
            {
                int randomIndex = Random.Range(0, pickups.Length);
                GameObject randPickup = pickups[randomIndex];
                Instantiate(randPickup, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}