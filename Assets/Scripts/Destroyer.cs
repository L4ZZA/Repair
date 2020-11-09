using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    public float lifeTime;

    private void Start()
    {
        if(lifeTime > 0)
        {
            Destroy(gameObject, lifeTime);
        }
    }

}
