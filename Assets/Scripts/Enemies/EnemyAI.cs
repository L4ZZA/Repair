using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float shootingRange = 1f;
    public float movementSpeed = 0.5f;

    public bool targetAcquired = false;
    private float dotProduct = 0;

    enum eStateMachine
    {
        chasing,
        aiming,
        shooting,
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        targetAcquired = isWithinRange();
    }

    bool isWithinRange()
    {
        var distanceFromTarget = target.position - transform.position;
        Debug.Log("Distance: " + distanceFromTarget);
        if (Mathf.Abs(distanceFromTarget.x) <= shootingRange || Mathf.Abs(distanceFromTarget.y) <= shootingRange)
            return true;

        return false;
    }
}
