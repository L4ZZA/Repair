using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    float shotTime;

    // Update is called once per frame
    void Update()
    {
        bool mouseLeftButtonDown = Input.GetMouseButton(0);
        if (mouseLeftButtonDown)
        {
            if (Time.time > shotTime)
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                shotTime = Time.time + timeBetweenShots;
            }
        }
    }
}
