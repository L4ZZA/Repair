using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComponentListVariable", menuName = "Custom Variables/ComponentList Variable")]
public class ComponentListVariable : ScriptableObject
{
    public List<Component> Value = new List<Component>();

    public void OnEnable()
    {
        ClearList();
    }

    public void ClearList()
    {
        Value = new List<Component>();
    }
}
