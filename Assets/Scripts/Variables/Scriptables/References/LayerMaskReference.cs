using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LayerMaskReference
{
    public bool UseConstant = true;
    public LayerMask ConstantValue;
    public LayerMaskVariable Variable;

    public LayerMaskReference()
    { }

    public LayerMaskReference(LayerMask value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public LayerMask Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator LayerMask(LayerMaskReference reference)
    {
        return reference.Value;
    }
}