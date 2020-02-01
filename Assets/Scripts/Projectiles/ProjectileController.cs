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

    private Ray rayMouse;
    private Vector3 mousePosition;
    private RaycastHit raycastHit;
    private Vector3 direction;
    private Quaternion rotation;

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



    private void SetTargetPosition()
    {
        if (targetSprite != null)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            targetSprite.gameObject.transform.position = mousePos;

            //targetSprite.gameObject.transform.rotation = Quaternion.FromToRotation(transform.up, Input.mousePosition);
        }
    }



    private void Rotate()
    {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);      
    }



    private void Update()
    {
        Rotate();
        SetTargetPosition();

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;

            var playerDirection = (mouse - transform.position).normalized;

            SpawnProjectileAtTarget(playerDirection);
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
