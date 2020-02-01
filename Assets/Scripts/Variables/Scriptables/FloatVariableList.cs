using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable List", menuName = "Custom Variables/FloatVariable List")]
public class FloatVariableList : ScriptableObject
{
    public List<FloatVariable> Value = new List<FloatVariable>();

    public void OnDisable()
    {
        //ClearList();
    }

    public void ClearList()
    {
        Value = new List<FloatVariable>();
    }
}
