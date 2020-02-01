using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entities
{
    public EntityType types;

    public enum EntityType
    {
        player,
        enemy,
        environment
    }
}

