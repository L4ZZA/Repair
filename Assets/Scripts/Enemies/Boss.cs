using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health;
    public Enemy[] enemies;
    public float spawnOffset;
    public int damage;
    public GameObject blood;
    public GameObject deathEffect;

    int halfHealth;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        halfHealth = health >> 1;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if(player)
                player.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            Instantiate(blood, transform.position, transform.rotation);
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if(health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }

        int randEnemyIndex = Random.Range(0, enemies.Length);
        Enemy randomEnemy = enemies[randEnemyIndex];
        var vOffset = new Vector3(spawnOffset, spawnOffset, 0);
        Instantiate(randomEnemy, transform.position + vOffset, transform.rotation);
    }
}
