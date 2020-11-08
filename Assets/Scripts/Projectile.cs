using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifeTime;
    public GameObject explosion;
    public GameObject soundObject;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyProjectile", lifeTime);
        Instantiate(soundObject, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void destroyProjectile()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            destroyProjectile();
        }
        if(collision.CompareTag("boss"))
        {
            var boss = collision.GetComponent<Boss>();
            boss.TakeDamage(damage);
            destroyProjectile();
        }
    }

}
