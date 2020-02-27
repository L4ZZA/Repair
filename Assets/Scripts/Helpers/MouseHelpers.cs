using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHelpers : MonoBehaviour
{
    /// <summary>
    /// Calculates the normalized vector from the given object location
    /// to the mouse, in screen coordinates.
    /// </summary>
    /// <param name="objPosition"></param>
    /// <returns></returns>
    public static Vector3 VecToMouse(Vector3 objPosition)
    {
        if (HUDController.paused)
            return objPosition;

        Vector3 screenOrigin = Camera.main.WorldToScreenPoint(objPosition);
        Vector3 dirToMouse = Input.mousePosition - screenOrigin;
        dirToMouse.Normalize();
        return dirToMouse;
    }

    /// <summary>
    /// Calculates the angle in radians between the given object and the mouse,
    /// using screen coordinates.
    /// </summary>
    /// <param name="objPosition">The positon of the reference object. Could be seen as pivot point.</param>
    /// <returns></returns>
    public static float ObjectToMouseRadians(Vector3 objPosition)
    {
        Vector3 dirToMouse = VecToMouse(objPosition);
        float angle = Mathf.Atan2(dirToMouse.y, dirToMouse.x);
        return angle;
    }

    /// <summary>
    /// Calculates the angle in degrees between the given object and the mouse,
    /// using screen coordinates.
    /// </summary>
    /// <param name="objPosition">The positon of the reference object. Could be seen as pivot point.</param>
    /// <returns></returns>
    public static float ObjectToMouseDegrees(Vector3 objPosition)
    {
        return ObjectToMouseRadians(objPosition) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Calculates the quaternion rotation between the given object and the mouse,
    /// using screen coordinates.
    /// </summary>
    /// <param name="objPosition">The positon of the reference object. Could be seen as pivot point.</param>
    /// <returns></returns>
    public static Quaternion ObjectToMouseRotation(Vector3 objPosition)
    {
        float angle = ObjectToMouseDegrees(objPosition);
        return Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
