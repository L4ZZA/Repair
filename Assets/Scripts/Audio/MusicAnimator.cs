using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAnimator : MonoBehaviour
{
    public Animator musicAnimator;
    public float waitTime = 2f;

    public IEnumerator FadeIn()
    {
        musicAnimator.SetTrigger("fadeIn");
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator FadeOut() 
    {
        musicAnimator.SetTrigger("fadeOut");
        yield return new WaitForSeconds(waitTime);
    }
}
