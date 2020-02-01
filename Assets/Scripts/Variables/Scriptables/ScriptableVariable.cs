using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Variable", menuName = "Custom Variables/Scriptable Variable")]
public class ScriptableVariable : ScriptableObject
{
    public ScriptableObject Value;
}
