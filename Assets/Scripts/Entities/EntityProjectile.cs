using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityProjectile : MonoBehaviour
{
    [Header("Damage / Force")]
    [SerializeField] private int damage;
    [SerializeField] private float force;

    [Header("Which layers to repair/damage?")]
    [SerializeField] private List<LayerMask> layersCanAffect = new List<LayerMask>();

    [Header("Collider / Rigidbody")]
    [SerializeField] private Collider2D collider;
    [SerializeField] private Rigidbody2D rigidBody;



    private void Awake()
    {
        if (collider == null)
        {
            collider = GetComponent<Collider2D>();

            if (collider == null)
            {
                Debug.LogError(this + ": you must assign collider.");
            }
        }

        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>();

            if (rigidBody == null)
            {
                Debug.LogError(this + ": you must assign rigidBody.");
            }
        }

        if (layersCanAffect.Count == 0)
        {
            Debug.LogError(this + ": you must add at least one entity type to damage.");
        }
    }


    public void FireProjectileForward(Transform _transform)
    {
        if (rigidBody != null)
        {
            rigidBody.AddForce(_transform.forward * force);
        }
    }


    public void FireProjectileAtTarget(RaycastHit2D _ray)
    {
        if (rigidBody != null)
        {
            Debug.Log("Force applied");
            rigidBody.AddForce(_ray.point * force);
        }
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObj = collision.gameObject;

        EntityHealth entityHealth = gameObj.GetComponent<EntityHealth>();

        if (entityHealth != null)
        {
            if (layersCanAffect.Count > 0)
            {
                foreach (var layer in layersCanAffect)
                {
                    if ((layer & 1 << gameObj.layer) == 1 << gameObj.layer)
                    {
                        DealDamage(entityHealth, damage);
                    }
                }
            }
        }
    }


    private void DealDamage(EntityHealth _entityHealth, int _damage)
    {
        _entityHealth.ChangeHealth(_damage);
    }
}
