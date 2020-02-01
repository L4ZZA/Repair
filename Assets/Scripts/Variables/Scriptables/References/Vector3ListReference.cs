using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Vector3ListReference
{
    public bool UseConstant = true;
    public List<Vector3Variable> ConstantValue;
    public Vector3ListVariable Variable;

    public Vector3ListReference()
    { }

    public Vector3ListReference(List<Vector3Variable> value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public List<Vector3Variable> Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator List<Vector3Variable>(Vector3ListReference reference)
    {
        return reference.Value;
    }
}