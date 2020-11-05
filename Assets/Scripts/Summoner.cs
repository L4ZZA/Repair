using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public float timeBtwSummons;
    public GameObject enemyToSummon;
    public float meleeAttackSpeed;
    public float stopDistance;

    Vector2 targetPosition;
    Animator anim;

    float summonTime;
    float attackTime;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        float randomX = Random.Range(minX, maxY);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (Vector2.Distance(transform.position, targetPosition) > 0.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);

                if( Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBtwSummons;
                    anim.SetTrigger("summon");
                }
            }

            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                if (Time.time >= attackTime)
                {
                    //attack
                    StartCoroutine(MeleeAttack());
                    attackTime = Time.time + timeBtwAttacks;
                }
            }
        }
    }

    public void Summon()
    {
        if (player)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }

    IEnumerator MeleeAttack()
    {
        player.GetComponent<Player>().TakeDamage(damage);
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float curveTime = 0f;
        //float curveAmount = attackAnimationCurve.Evaluate(curveTime);
        while (curveTime < 1.0f)
        {
            curveTime += Time.deltaTime * meleeAttackSpeed;
            float curveAmount = attackAnimationCurve.Evaluate(curveTime);
            transform.position = Vector2.Lerp(originalPosition, targetPosition, curveAmount);
            yield return null;
        }
    }
}
