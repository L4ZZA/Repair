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

    private Vector3 mLastMousePos;
    private float mRotation;

    private void Awake()
    {
        mLastMousePos = Input.mousePosition;
        mRotation = 0f;
        Debug.Log("AWAKE - rotation: " + mRotation);
        if (projectilePrefab == null)
        {
            Debug.LogError(this + ": must added projectile.");
        }

        if (firingPosition == null)
        {
            Debug.LogError(this + ": must add firing position.");
        }
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
         
         //Get the Screen positions of the object
         Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
         
         //Get the Screen position of the mouse
         Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
         
         //Get the angle between the points
         float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        //Ta Daaa
        //transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));
     }
 
     float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
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
