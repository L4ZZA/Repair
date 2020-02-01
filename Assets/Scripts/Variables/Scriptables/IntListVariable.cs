using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntListVariable", menuName = "Custom Variables/IntList Variable")]
public class IntListVariable : ScriptableObject
{
    public List<int> Value = new List<int>();

    public void OnEnable()
    {
        ClearList();
    }

    public void ClearList()
    {
        Value = new List<int>();
    }
}
