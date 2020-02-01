using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public float force = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rigidBody.AddForce(transform.up * force);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            rigidBody.AddForce(transform.right * -force);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            rigidBody.AddForce(transform.up * -force);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rigidBody.AddForce(transform.right * force);
        }

    }
}
