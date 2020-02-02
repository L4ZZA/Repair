using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation2D : MonoBehaviour
{
    [SerializeField]
    public Transform Pivot;

    [SerializeField]
    public Transform TargetToLookAt;

    [SerializeField] 
    public bool followMouse = false;

    void Start()
    {
        if (!TargetToLookAt)
            followMouse = true;

        transform.parent = Pivot.transform;
    }

    void Update()
    {
        Vector3 PivotVector = Camera.main.WorldToScreenPoint(Pivot.position);
        if (followMouse)
        {
            PivotVector = Input.mousePosition - PivotVector;
        }
        else
        {
            PivotVector = TargetToLookAt.position - PivotVector;
        }

        float angle = Mathf.Atan2(PivotVector.y, PivotVector.x) * Mathf.Rad2Deg;
        Pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
