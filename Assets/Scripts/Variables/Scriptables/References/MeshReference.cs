using System;
using UnityEngine;

[Serializable]
public class MeshReference
{
    public bool UseConstant = true;
    public Mesh ConstantValue;
    public MeshVariable Variable;

    public MeshReference()
    { }

    public MeshReference(Mesh value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public Mesh Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator Mesh(MeshReference reference)
    {
        return reference.Value;
    }
}