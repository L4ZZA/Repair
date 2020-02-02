using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Projectile prefab")]
    [SerializeField] private GameObject projectilePrefab;

    [Header("Firing position")]
    [SerializeField] private GameObject firingPosition;

    [Header("Rotation speed")]
    [SerializeField] private float rotationSpeed;

    [Header("Target sprite")]
    [SerializeField] private SpriteRenderer targetSprite;

    [SerializeField]
    public Transform orb;

    [SerializeField] public float radius;

    private Transform pivot;

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
        pivot = orb.transform;
        transform.parent = pivot;
        transform.position += Vector3.up * radius;
    }

    void Update()
    {
        Vector3 orbVector = Camera.main.WorldToScreenPoint(orb.position);
        orbVector = Input.mousePosition - orbVector;
        float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;

        pivot.position = orb.position;
        pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        SetTargetPosition();

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;

            var playerDirection = (mouse - transform.position).normalized;

            SpawnProjectileAtTarget(playerDirection);
        }
    }

    private void SetTargetPosition()
    {
        if (targetSprite != null)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            targetSprite.gameObject.transform.position = mousePos;

            //targetSprite.gameObject.transform.rotation = Quaternion.FromToRotation(transform.up, Input.mousePosition);
        }
    }
    
    public void SpawnProjectileAtTarget(Vector3 _playerDirection)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, firingPosition.transform.position, Quaternion.identity);
            EntityProjectile entityProjectile = projectileObject.GetComponent<EntityProjectile>();

            if (entityProjectile != null)
            {
                entityProjectile.FireProjectileAtTarget(_playerDirection);
            }

            else Debug.Log(this + ": EntityProjectile was not found on this prefab.");
        }
    }


}
