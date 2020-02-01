using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Transform Variable", menuName = "Custom Variables/Transform Variable")]
public class TransformVariable : ScriptableObject
{
    public Transform Value;
}