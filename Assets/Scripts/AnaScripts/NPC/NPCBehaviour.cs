using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour, ITakeDamage
{

    private NavMeshAgent agent;
    private GameObject target;
    [SerializeField]
    private GameObject safeTarget;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsTarget;
    [SerializeField]
    private int health;

    bool safe = false;
    bool following = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
        
    }

    
    public void SetEndState()
    {
        target = safeTarget;
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
            SetTarget(other.gameObject);
            following = true;
        }

        if (other.tag == "SafeRoom")
        {
            SetEndState();
            safe = true;
            following = false;
        }
    }

    void Update()
    {
        if (following)
        {
            agent.SetDestination(target.transform.position);
        }

        if (safe)
        {
            agent.SetDestination(target.transform.position);
        }
    }
}
