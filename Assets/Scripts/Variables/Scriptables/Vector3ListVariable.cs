using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector3ListVariable", menuName = "Custom Variables/Vector3 List Variable")]
public class Vector3ListVariable : ScriptableObject
{
    public List<Vector3Variable> Value = new List<Vector3Variable>();
}
