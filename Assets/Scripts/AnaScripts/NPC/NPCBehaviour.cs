using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour, ITakeDamage
{

    private NavMeshAgent agent;
    private Transform target;
    [SerializeField]
    private Transform safeTarget;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsTarget;
    [SerializeField]
    private int health;

    bool safe = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
        agent.SetDestination(target.position);
    }

    
    public void SetEndState()
    {
        target = safeTarget;
        agent.SetDestination(target.position);
    }

    public void TakeDamage()
    {
        if (health > 0)
            health -= 1;
        //else GetComponent<QuestCompletion>().Fail();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.tag == "Player"&&safe==false)
        {
            SetTarget(other.transform);
        }

        if (other.tag == "SafeRoom")
        {
            SetEndState();
            safe = true;
        }
    }
}
