using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public Weapon weaponToEquip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Instantiate(effect, transform.position, transform.rotation);
            player.ChangeWeapon(weaponToEquip);
            Destroy(gameObject);
        }
    }
}
