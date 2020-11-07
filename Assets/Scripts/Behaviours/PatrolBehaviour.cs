using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    public float speed;

    GameObject[] patrolPoints;
    int randomPoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolPoints = GameObject.FindGameObjectsWithTag("patrolPoint");
        randomPoint = Random.Range(0, patrolPoints.Length);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 origin = animator.transform.position;
        Vector2 destination = patrolPoints[randomPoint].transform.position;
        animator.transform.position = Vector2.MoveTowards(
            origin,
            destination,
            speed * Time.deltaTime);

        if(Vector2.Distance(origin, destination) < 0.1f)
        {
            int tmpPoint;
            do
            {
                tmpPoint = Random.Range(0, patrolPoints.Length);
            }
            while(tmpPoint == randomPoint);
            randomPoint = tmpPoint;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
