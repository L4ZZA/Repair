using System;
using UnityEngine;

[Serializable]
public class TextureReference
{
    public bool UseConstant = true;
    public Texture ConstantValue;
    public TextureVariable Variable;

    public TextureReference()
    { }

    public TextureReference(Texture value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public Texture Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator Texture(TextureReference reference)
    {
        return reference.Value;
    }
}