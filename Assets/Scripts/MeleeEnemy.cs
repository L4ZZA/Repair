using System.Collections;
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
                    attackTime = Time.time + timeBtwAttacks;
                    StartCoroutine(MeleeAttack());
                }
            }
        }
    }

    //IEnumerator MeleeAttack()
    //{
    //    player.GetComponent<Player>().TakeDamage(damage);
    //    Vector2 originalPosition = transform.position;
    //    Vector2 targetPosition = player.position;

    //    float percent = 0f;
    //    while (percent <= 1)
    //    {
    //        percent += Time.deltaTime * attackSpeed;
    //        float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
    //        transform.position = Vector2.Lerp(originalPosition, targetPosition, interpolation);
    //        yield return null;
    //    }
    //}


    IEnumerator MeleeAttack()
    {
        player.GetComponent<Player>().TakeDamage(damage);
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float curveTime = 0f;
        float curveAmount = attackAnimationCurve.Evaluate(curveTime);
        while (curveTime < 1.0f)
        {
            curveTime += Time.deltaTime * attackSpeed;
            curveAmount = attackAnimationCurve.Evaluate(curveTime);
            transform.position = Vector2.Lerp(originalPosition, targetPosition, curveAmount);
            yield return null;
        }
    }
}
