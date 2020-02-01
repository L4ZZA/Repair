using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClip Variable", menuName = "Custom Variables/AudioClip Variable")]
public class AudioClipVariable : ScriptableObject
{
    public AudioClip Value;
}