using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityProjectile : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage;

    [Header("Which layers to repair/damage?")]
    [SerializeField] private List<LayerMask> layersCanAffect = new List<LayerMask>();

    [Header("Damage")]
    [SerializeField] private Collider2D collider;


    private void Awake()
    {
        if (collider == null)
        {
            collider = GetComponent<Collider2D>();

            if (collider == null)
            {
                Debug.LogError(this + ": you must assign collider");
            }
        }

        if (layersCanAffect.Count == 0)
        {
            Debug.LogError(this + ": you must add at least one entity type to damage");
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
