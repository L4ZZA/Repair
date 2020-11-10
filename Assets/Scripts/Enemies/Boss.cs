using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    public Enemy[] enemies;
    public float spawnOffset;
    public int damage;
    public GameObject blood;
    public GameObject deathEffect;
    public GameObject figure;
    public HealthBar healthBar;

    int halfHealth;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        halfHealth = health >> 1;
        anim = GetComponent<Animator>();
        healthBar.gameObject.SetActive(true);
        healthBar.SetMaxHealth(health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if(player && figure.activeInHierarchy)
                player.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.SetHealth(health);

        if(health <= 0)
        {
            Instantiate(blood, transform.position, transform.rotation);
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            healthBar.gameObject.SetActive(false);
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
