using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Projectile Prefab")]
    [SerializeField] private GameObject projectilePrefab;

    [Header("Firing Position")]
    [SerializeField] private GameObject firingPosition;

    [Header("Rotation Speed")]
    [SerializeField] private float rotationSpeed;

    public Ray rayMouse;
    public Vector3 mousePosition;
    public RaycastHit raycastHit;
    public Vector3 direction;
    public Quaternion rotation;

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


    private void RotateToMouse(GameObject _gameObject, Vector3 _destination)
    {
        direction = _destination - _gameObject.transform.position;
        rotation = Quaternion.LookRotation(direction);
        _gameObject.transform.localRotation = Quaternion.Lerp(_gameObject.transform.rotation, rotation, 1);
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

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            SpawnProjectileAtTarget(rayHit);
        }
    }
     

    public void SpawnProjectileAtTarget(RaycastHit2D _rayhit)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, firingPosition.transform.position, Quaternion.identity);
            EntityProjectile entityProjectile = projectileObject.GetComponent<EntityProjectile>();

            if (entityProjectile != null)
            {
                //entityProjectile.FireProjectileAtTarget(_rayhit);
                entityProjectile.FireProjectileForward(this.transform);
            }

            else Debug.Log(this + ": EntityProjectile was not found on this prefab.");
        }
    }


}
