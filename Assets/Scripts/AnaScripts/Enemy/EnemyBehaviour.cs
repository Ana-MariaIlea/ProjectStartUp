﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    // Animator guide
    //
    // IdleAnimation   0
    // WalkAnimation   1
    // DeathAnimation  2
    // ScreamAnimation 3
    // RunAnimation    4
    // AttackAnimation 5


    [System.Serializable]
    public class PathWay
    {
        public Transform pathHolder;
        public float timeToStay = 4;

    }
    [SerializeField]
    private PathWay[] path;
    [SerializeField]
    private GameObject player = null;

    private Animator anim;

    private int walkPoint = -1;
    private bool walkPointSet;

    private NavMeshAgent agent;
    private Transform target;
    private Transform hostage = null;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsTarget;
    [SerializeField]
    private LayerMask whatIsObstacle;

    [SerializeField]
    private float sightRange;
    [SerializeField]
    private float chaseRange;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float viewAngle;
    [SerializeField]
    private bool targetInSight, targetInAttackRange;

    [SerializeField]
    float attackTimer;
    [SerializeField]
    int damageDone;
    [SerializeField]
    int walkSpeed;
    [SerializeField]
    int runSpeed;
    int playerState = 0;
    float timer = 5;
    float timerforAttack;
    float range = 0;
    EnemyManager enemyManager;
    //----------------------------------------------------------------
    //                  Draw Gizmos
    //----------------------------------------------------------------
    private void OnDrawGizmos()
    {
        foreach (PathWay waypoint in path)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(waypoint.pathHolder.position, .1f);
        }

        Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.yellow;
        // Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);

    }
    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.speed = walkSpeed;
        timerforAttack = attackTimer;
        range = sightRange;
    }



    void Update()
    {

       // Debug.Log("Scream   "+anim.GetInteger("scream"));
        if (hostage == null)
        {
            Collider[] hitCollidersSight = Physics.OverlapSphere(transform.position, range, whatIsTarget);
            if (hitCollidersSight.Length >= 1)
            {
                Vector3 directionToTarget = (hitCollidersSight[0].transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, hitCollidersSight[0].transform.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distToTarget, whatIsObstacle))
                    {
                        targetInSight = true;
                        target = hitCollidersSight[0].transform;
                        if (anim.GetInteger("scream") == 0 && anim.GetInteger("condition") != 3)
                        {
                            agent.SetDestination(transform.position);

                            anim.SetInteger("condition", 3);
                        }
                        else if (anim.GetInteger("scream") == 1)
                        {
                            anim.SetInteger("condition", 4);
                        }

                    }

                }

            }
            else { targetInSight = false; }

            Collider[] hitCollidersAttack = Physics.OverlapSphere(transform.position, attackRange, whatIsTarget);
            if (hitCollidersAttack.Length >= 1)
            {
                targetInAttackRange = true;
                target = hitCollidersAttack[0].transform;
                anim.SetInteger("condition", 5);
            }
            else { targetInAttackRange = false; }

            if (playerState == 1) /// CHANGE, get player reference, player needs state to indicate that it is making sound
            {
                Vector3 position = new Vector3(0, 0, 0);
                CalculateNoisePath(position);
            }
           // Debug.Log("condition   " + anim.GetInteger("condition"));

            if (!targetInSight && !targetInAttackRange) Patroling();
            if (targetInSight && !targetInAttackRange) Chase();
            if (targetInSight && targetInAttackRange)
            {
                Attack();
            }
        }
    }

    float CalculateNoisePath(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        if (agent.enabled)
        {
            agent.CalculatePath(targetPosition, path);
        }
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }
        float pathLength = 0;
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }

    void Patroling()
    {
        // Debug.Log("patrol");
        if (walkPointSet == false)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(target.position);
            anim.SetInteger("condition", 1);
            anim.SetInteger("scream", 0);
        }

        Vector3 distanceToLocation = transform.position - target.position;


        if (distanceToLocation.magnitude < 1f)
        {
            // walkPointSet = false;

            StayPut();

        }

    }

    void StayPut()
    {
        anim.SetInteger("condition", 0);
        agent.SetDestination(transform.position);
        if (timer <= 0)
        {
            walkPointSet = false;
            //timer = 5;
        }
        else
        {
            timer -= Time.fixedDeltaTime;
        }
        // Debug.Log(timer);
    }

    void SearchWalkPoint()
    {
        walkPoint++;
        if (walkPoint > path.Length - 1 || walkPoint < 0) walkPoint = 0;
        target = path[walkPoint].pathHolder;
        timer = path[walkPoint].timeToStay;
        walkPointSet = true;
        if (range != sightRange) range = sightRange;
    }
    void Chase()
    {
        agent.speed = runSpeed;
        walkPointSet = false;
        if (anim.GetInteger("condition") == 4 && anim.GetInteger("scream") == 1)
            agent.SetDestination(target.position);
        else if (anim.GetInteger("scream") == 0) agent.SetDestination(transform.position);
        range = chaseRange;
    }
    void Attack()
    {
        if (timerforAttack <= 0)
        {
            agent.SetDestination(transform.position);
            transform.LookAt(target);
            anim.SetInteger("condition", 5);
            GetComponentInChildren<EnemyScreamChange>().SetParamForAttack(target, damageDone);
            timerforAttack = attackTimer;
        }
        else timerforAttack -= Time.fixedDeltaTime;

    }




    public void EnemyGetsHit(Transform transform)
    {
        Debug.Log("Hits Enemy");
        target = transform;
        Attack();
    }

    public void EnemyDies()
    {
        anim.SetInteger("condition", 2);
        range = 0;
        agent.SetDestination(transform.position);
        enemyManager.EnemyDies();
    }

}
