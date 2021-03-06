using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation2D : MonoBehaviour
{
    [SerializeField]
    public Transform TargetToLookAt;

    [SerializeField]
    public bool followMouse = false;

    void Start()
    {
        if (!TargetToLookAt)
        {
            followMouse = true;
        }

    }

    void Update()
    {
        Vector3 pivotVector;
        if (followMouse)
        {
            pivotVector = MouseHelpers.VecToMouse(transform.position);
        }
        else
        {
            pivotVector = TargetToLookAt.position - transform.position;
            pivotVector.Normalize();
        }

        float angle = Mathf.Atan2(pivotVector.y, pivotVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
