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

    [SerializeField]
    private SpriteRenderer playerHalo;

    [Header("Object Transform")]
    [SerializeField]
    public Transform Origin;

    [SerializeField]
    private GameObject pillSpriteObject;
    private bool repairBulletSelected;

    [Header("Change Weapons")]
    [SerializeField] KeyCode weaponButton = KeyCode.LeftShift;

    private Color repairColor = Color.HSVToRGB(167f/360f, 1f, 1f);
    private Color destroyColor = Color.HSVToRGB(0f, 1f, 1f);

    private void Awake()
    {
        repairBulletSelected = true;
        playerHalo.color = repairColor;

        if (projectilePrefab == null)
        {
            Debug.LogError(this + ": must added projectile.");
        }

        if (firingPosition == null)
        {
            Debug.LogError(this + ": must add firing position.");
        }
    }

    void Update()
    {
        if (HUDController.paused)
            return;

        Vector3 dirToMouse = MouseHelpers.VecToMouse(Origin.position);
        Origin.rotation = MouseHelpers.ObjectToMouseRotation(Origin.position);


        if (Input.GetMouseButtonDown(0))
        {
            SpawnProjectileAtTarget(dirToMouse);
        }

        if (Input.GetKeyDown(weaponButton))
        {
            repairBulletSelected = !repairBulletSelected;

            if (repairBulletSelected)
            {
                ChangePillDirection(pillSpriteObject, 0);
                playerHalo.color = repairColor;
            }
            else
            {
                ChangePillDirection(pillSpriteObject, 180);
                playerHalo.color = destroyColor;
            }

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
                if (repairBulletSelected)
                {
                    entityProjectile.ChangeBulletToRepair();
                }

                else
                {
                    entityProjectile.ChangeBulletToDamage();
                }

                entityProjectile.FireProjectileAtTarget(_playerDirection);
            }
            else
            {
                Debug.LogError(this + ": EntityProjectile was not found on this prefab.");
            }
        }
        else
        {
            Debug.LogError("projectilePrefab not null");
        }
    }


    private void ChangePillDirection(GameObject _gameObject, float _rotation)
    {
        if (_gameObject)
            _gameObject.transform.localRotation = Quaternion.Euler(0, 0, _rotation);
        else
            Debug.LogError(this + "_gameObject is null.");
    }
}
