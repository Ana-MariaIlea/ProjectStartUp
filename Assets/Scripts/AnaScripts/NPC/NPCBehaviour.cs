using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

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
    [SerializeField]
    private TextMeshProUGUI helpText;

    bool safe = false;
    bool following = false;
    bool listening = false;


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

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && safe == false)
        {
            if (following == true)
            {
                helpText.text = "Press Q to stop following";
            }
            else
            {
                helpText.text = "Press E to start following";
            }
            // Debug.Log("OnTriggerEnter NPC");
            if (Input.GetKeyDown(KeyCode.E))
            {
                listening = true;
                following = true;
                SetTarget(other.gameObject);
                //following = true;
               // CapsuleCollider col = GetComponent<CapsuleCollider>();
                //col.radius = 0.5f;
               // col.center = new Vector3(0, 1, 0);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                listening = false;
                following = false;
                SetTarget(this.gameObject);
                //following = true;
               // CapsuleCollider col = GetComponent<CapsuleCollider>();
               // col.radius = 1.5f;
               // col.center = new Vector3(0, 1.5f, 0);
            }

            //Debug.Log("Follow player");
            
        }

        if (other.tag == "Volume")
        {
            SetEndState();
            safe = true;
            following = false;
            GetComponent<QuestCompletion>().Compltion();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && safe == false)
        {
            helpText.text = "";
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
