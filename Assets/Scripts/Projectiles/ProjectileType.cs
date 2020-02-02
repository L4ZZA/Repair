using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Projectile
{
    public ProjectileType projectile;

    public enum ProjectileType
    {
        repair,
        damage
    }
}

