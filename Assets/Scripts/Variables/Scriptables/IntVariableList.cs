using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable List", menuName = "Custom Variables/IntVariable List")]
public class IntVariableList : ScriptableObject
{
    public List<IntVariable> Value = new List<IntVariable>();

    public void OnDisable()
    {
        ClearList();
    }

    public void ClearList()
    {
        Value = new List<IntVariable>();
    }
}
