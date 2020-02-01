using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageDealer : MonoBehaviour
{
    [SerializeField] private Collider2D collider;
    [SerializeField] private List<Entities> entitiesCanChange = new List<Entities>();

    private void Awake()
    {
        if (collider == null)
        {
            Debug.LogError(this + ": you must assign collider");
        }

        if (entitiesCanChange.Count == 0)
        {
            Debug.LogError(this + ": you must add at least one entity type to damage");
        }
    }


    public void OnCollisionEnter(Collision other)
    {
        GameObject gameObj = other.gameObject;

        EntityHealth entityHealth = gameObj.GetComponent<EntityHealth>();

        if (entityHealth != null)
        {
            entityHealth.ChangeHealth(0);
        }
    }
}
