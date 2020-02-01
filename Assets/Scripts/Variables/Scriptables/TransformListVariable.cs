using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransformListVariable", menuName = "Custom Variables/Transform List Variable")]
public class TransformListVariable : ScriptableObject
{
    public List<Transform> Value = new List<Transform>();
}
