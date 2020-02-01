using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class FloatListReference
{
    public bool UseConstant = true;
    public List<FloatVariable> ConstantValue;
    public FloatVariableList Variable;

    public FloatListReference()
    { }

    public FloatListReference(List<FloatVariable> value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public List<FloatVariable> Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator List<FloatVariable>(FloatListReference reference)
    {
        return reference.Value;
    }
}