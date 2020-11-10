using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public int healAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(player && collision.CompareTag("Player"))
        {
            Instantiate(effect, transform.position, transform.rotation);
            player.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
