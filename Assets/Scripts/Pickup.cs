using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float lifeTime;

    public GameObject effect;

    float lifeTimePassed = 0f;
    const float blinkStartTime = 3f;
    protected Player player;
    Blink blinkEffect;

    private void Start()
    {
        var obj = GameObject.FindGameObjectWithTag("Player");
        if(obj)
        {
            player = obj.GetComponent<Player>();
        }
        blinkEffect = GetComponent<Blink>();
    }

    private void Update()
    {
        if(lifeTime > 0f)
        {
            lifeTimePassed += Time.deltaTime;
            if(lifeTime - lifeTimePassed <= blinkStartTime)
            {
                blinkEffect.DoBlink();
            }

        }
        else
        {
            blinkEffect.DoBlink();
        }
    }
}
