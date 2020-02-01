using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material Variable", menuName = "Custom Variables/Material Variable")]
public class MaterialVariable : ScriptableObject
{
    public Material Value;
}