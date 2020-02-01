using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mesh Variable", menuName = "Custom Variables/Mesh Variable")]
public class MeshVariable : ScriptableObject
{
    public Mesh Value;
}