using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    //[Header("Projectile prefab")]
    //[SerializeField] private GameObject projectilePrefab;

    //[Header("Firing position")]
    //[SerializeField] private GameObject firingPosition;

    //[Header("Rotation speed")]
    //[SerializeField] private float rotationSpeed;

    //[Header("Target sprite")]
    //[SerializeField] private SpriteRenderer targetSprite;

    [SerializeField]
    public Transform Pivot;

    public Transform TargetToLookAt;

    [SerializeField] public bool followMouse = false;

    private Transform pivot;

    void Start()
    {
        if (!TargetToLookAt)
            followMouse = true;

        pivot = Pivot.transform;
        transform.parent = pivot;
        transform.position += Vector3.up;
    }

    void Update()
    {
        Vector3 PivotVector = Camera.main.WorldToScreenPoint(Pivot.position);
        if (followMouse)
            PivotVector = Input.mousePosition - PivotVector;
        else
        {
            Debug.Log("---" + TargetToLookAt.position);
            PivotVector = TargetToLookAt.position - PivotVector;
        }
        float angle = Mathf.Atan2(PivotVector.y, PivotVector.x) * Mathf.Rad2Deg;

        pivot.position = Pivot.position;
        pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //SetTargetPosition();

        //if (Input.GetMouseButtonDown(0))
        //{
        //    SpawnProjectileAtTarget(PivotVector.normalized);
        //}
    }

    //private void SetTargetPosition()
    //{
    //    if (targetSprite != null)
    //    {
    //        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    //        targetSprite.gameObject.transform.position = mousePos;

    //        //targetSprite.gameObject.transform.rotation = Quaternion.FromToRotation(transform.up, Input.mousePosition);
    //    }
    //}
    
    //public void SpawnProjectileAtTarget(Vector3 _playerDirection)
    //{
    //    if (projectilePrefab != null)
    //    {
    //        GameObject projectileObject = Instantiate(projectilePrefab, firingPosition.transform.position, pivot.rotation);
    //        EntityProjectile entityProjectile = projectileObject.GetComponent<EntityProjectile>();

    //        if (entityProjectile != null)
    //        {
    //            entityProjectile.FireProjectileAtTarget(_playerDirection);
    //        }

    //        else Debug.Log(this + ": EntityProjectile was not found on this prefab.");
    //    }
    //}


}
