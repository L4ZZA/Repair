using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public float fadeSpeed = 1f;
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
        currentColor.a -= Time.deltaTime * fadeSpeed;
        sprite.color = currentColor;
        if(currentColor.a <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
