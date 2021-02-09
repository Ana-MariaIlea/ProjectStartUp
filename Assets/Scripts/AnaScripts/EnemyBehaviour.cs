using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    enum enemyState
    {
        PATROL,
        CHASE,
        SEARCH,
        ATTACK,
        GOBACK
    }
    enemyState currentState;

    [SerializeField]
    private Transform[] pathHolder;

    private int walkPoint=0;
    private bool walkPointSet;

    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsTarget;

    [SerializeField]
    private float sightRange;
    [SerializeField]
    private float chaseRange;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private bool targetInSight, targetInAttackRange;
    //----------------------------------------------------------------
    //                  Draw Gizmos
    //----------------------------------------------------------------
    private void OnDrawGizmos()
    {
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(waypoint.position, .1f);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }



    void Update()
    {
        Collider[] hitCollidersSight = Physics.OverlapSphere(transform.position, sightRange, whatIsTarget);
        //Debug.Log(hitCollidersSight.Length);
        if (hitCollidersSight.Length >= 1)
        {
            targetInSight = true;
            target = hitCollidersSight[0].transform;
        }
        else { targetInSight = false; }

        Collider[] hitCollidersAttack = Physics.OverlapSphere(transform.position, attackRange, whatIsTarget);
        ///Debug.Log(hitCollidersAttack.Length);
        if (hitCollidersAttack.Length >= 1)
        {
            targetInAttackRange = true;
            target = hitCollidersAttack[0].transform;
        }
        else { targetInAttackRange = false; }
        //Debug.Log(targetInSight + "  " + targetInAttackRange);
        

        if (!targetInSight && !targetInAttackRange) Patroling();
        if (targetInSight && !targetInAttackRange) Chase();
        if (targetInSight && targetInAttackRange) Attack();
    }

    void Patroling()
    {
        Debug.Log("patrol");
        if (walkPointSet==false) SearchWalkPoint();
        else
        {
            agent.SetDestination(target.position);
        }

        Vector3 distanceToLocation = transform.position - target.position;


        if (distanceToLocation.magnitude < 1f)
        {
            walkPointSet = false;
        }
        
    }

    void SearchWalkPoint()
    {
        walkPoint++;
        if (walkPoint > pathHolder.Length - 1) walkPoint = 0;
        target = pathHolder[walkPoint];
        walkPointSet = true;
    }
    void Chase()
    {
        walkPointSet = false;
        agent.SetDestination(target.position);
    }
    void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(target);
        target.GetComponent<ITakeDamage>().TakeDamage();
    }

}
