using System;
using UnityEngine;

[Serializable]
public class ColorReference
{
    public bool UseConstant = true;
    public Color ConstantValue;
    public ColorVariable Variable;

    public ColorReference()
    { }

    public ColorReference(Color value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public Color Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator Color(ColorReference reference)
    {
        return reference.Value;
    }
}