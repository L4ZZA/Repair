using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class TransformListReference
{
    public bool UseConstant = true;
    public List<Transform> ConstantValue = new List<Transform>();
    public TransformListVariable Variable;

    public TransformListReference()
    { }

    public TransformListReference(List<Transform> value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public List<Transform> Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator List<Transform>(TransformListReference reference)
    {
        return reference.Value;
    }
}