using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    
    [SerializeField] 
    private GameObject projectilePrefab;
    
    [SerializeField] 
    private GameObject firingPosition;
    
    [SerializeField] 
    private SpriteRenderer targetSprite;

    [Header("Object Transform")]
    [SerializeField]
    public Transform Origin;

    private void Awake()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError(this + ": must added projectile.");
        }

        if (firingPosition == null)
        {
            Debug.LogError(this + ": must add firing position.");
        }
    }

    void Start()
    {
        transform.parent = Origin.transform;
    }

    void Update()
    {
        Vector3 OriginVector = Camera.main.WorldToScreenPoint(Origin.position);
        OriginVector = Input.mousePosition - OriginVector;
        float angle = Mathf.Atan2(OriginVector.y, OriginVector.x) * Mathf.Rad2Deg;

        Origin.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);


        if (Input.GetMouseButtonDown(0))
        {
            SpawnProjectileAtTarget(OriginVector.normalized);
        }
    }
    
    public void SpawnProjectileAtTarget(Vector3 _playerDirection)
    {
        if (projectilePrefab)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, firingPosition.transform.position, Origin.rotation);
            EntityProjectile entityProjectile = projectileObject.GetComponent<EntityProjectile>();

            if (entityProjectile)
            {
                entityProjectile.FireProjectileAtTarget(_playerDirection);
            }
            else
            {
                Debug.Log(this + ": EntityProjectile was not found on this prefab.");
            }
        }
        else
        {
            Debug.Log("projectilePrefab not null");
        }
    }


}
