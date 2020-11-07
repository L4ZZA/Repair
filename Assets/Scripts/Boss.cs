using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health;
    public Enemy[] enemies;
    public float spawnOffset;
    public int damage;

    int halfHealth;
    Animator anim;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        halfHealth = health >> 1;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        { player.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
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
