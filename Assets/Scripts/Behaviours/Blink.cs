using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public float speed;
    public AnimationCurve animationCurve;
    public SpriteRenderer sprite;
    bool blinking = false;
    float initialAlpha;

    private void Start()
    {
        initialAlpha = sprite.color.a;
    }

    public void DoBlink()
    {
        StartCoroutine(Blinking());
    }

    IEnumerator Blinking()
    {
        blinking = true;
        float curveTime = 0f;
        while(true)
        {
            curveTime += Time.deltaTime * speed;
            float curveAmount = animationCurve.Evaluate(curveTime);
            var newColor = sprite.color;
            float alpha = Mathf.Lerp(initialAlpha, 1f - initialAlpha, curveAmount);
            newColor.a = alpha;
            sprite.color = newColor;
            yield return null;
        }
    }

    public bool IsBlinking()
    {
        return blinking;
    }
}
