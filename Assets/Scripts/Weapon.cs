using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    float shotTime;
    Animator cameraAnim;

    // Update is called once per frame
    void Start()
    {
        cameraAnim = Camera.main.GetComponent<Animator>();
    }

    void Update()
    {
        bool mouseLeftButtonDown = Input.GetMouseButton(0);
        if (mouseLeftButtonDown)
        {
            if (Time.time > shotTime)
            {
                cameraAnim.SetTrigger("shotShake");
                Instantiate(projectile, shotPoint.position, transform.rotation);
                shotTime = Time.time + timeBetweenShots;
            }
        }
    }
}
