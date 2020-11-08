using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public float fadeSpeed = 1f;
    public float minFadeValue;
    // Makes the blood stain disappear after the fade has completed.
    public bool persist;
    Color currentColor = Color.white;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = currentColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentColor.a <= minFadeValue)
        {
            if(!persist)
                Destroy(gameObject);
        }
        else
        {
            currentColor.a -= Time.deltaTime * fadeSpeed;
            sprite.color = currentColor;            
        }
    }
}
