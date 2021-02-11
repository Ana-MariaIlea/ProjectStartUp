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
    [SerializeField]
    private int offset;

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
        else GetComponent<QuestCompletion>().Fail();
    }

    public void OnTriggerEnter(Collider other)
    {
       // Debug.Log("OnTriggerEnter NPC");
        if (other.tag == "Player"&&safe==false)
        {
            //Debug.Log("Follow player");
            SetTarget(other.gameObject);
            following = true;
            CapsuleCollider col = GetComponent<CapsuleCollider>();
            col.radius = 0.5f;
            col.center = new Vector3(0, 1, 0);
        }

        if (other.tag == "Volume")
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
            Vector3 distance = transform.position - target.transform.position;
            if (distance.magnitude < offset)
            {
                agent.SetDestination(transform.position);
            }
            else
            {
                agent.SetDestination(target.transform.position);
            }
            
        }

        if (safe)
        {
            agent.SetDestination(target.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            GetComponent<QuestCompletion>().Fail();
            Destroy(this.gameObject);
        }
    }
}
