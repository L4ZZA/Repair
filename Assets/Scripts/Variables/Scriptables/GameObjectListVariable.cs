using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectList Variable", menuName = "Custom Variables/GameObjectList Variable")]
public class GameObjectListVariable : ScriptableObject
{
    public List<GameObject> Value = new List<GameObject>();

    public void ClearList()
    {
        Value = new List<GameObject>();
    }
}
