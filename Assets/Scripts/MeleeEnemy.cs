using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float stopDistance;
    public float attackSpeed;
    
    float attackTime;

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                if(Time.time >= attackTime)
                {
                    //attack
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBtwAttacks;
                }
            }
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0f;
        while(percent <= 1f)
        {
            percent += Time.deltaTime * attackSpeed;
            float parabolicCurve = (-Mathf.Pow(percent, 2f) + percent) * 4f;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, parabolicCurve);
            yield return null;
        }
    }
}
