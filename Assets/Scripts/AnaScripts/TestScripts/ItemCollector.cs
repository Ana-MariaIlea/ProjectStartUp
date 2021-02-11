using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private LayerMask whatIsItem;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
       // Debug.Log("Trigger   "+ other.gameObject.name);
        if (other.gameObject.tag == "Item")
        {
            //Debug.Log("Trigger Item");
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.GetComponent<QuestCompletion>().Compltion();
                Destroy(other.gameObject);
                //Debug.Log("Destroy Item");
            }
        }
    }
}
