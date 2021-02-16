using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private LayerMask whatIsItem;
    [SerializeField]
    private TextMeshProUGUI helpText;

    private void OnTriggerStay(Collider other)
    {
       // Debug.Log("Trigger   "+ other.gameObject.name);
        if (other.gameObject.tag == "Item")
        {
            helpText.text = "Press E to colect";
            //Debug.Log("Trigger Item");
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.GetComponent<QuestCompletion>().Compltion();

                if (other.gameObject.GetComponent<ItemDestroyObject>())
                {
                    other.gameObject.GetComponent<ItemDestroyObject>().DestroyObjects();
                    helpText.text = "";
                }
                Destroy(other.gameObject);
                //Debug.Log("Destroy Item");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            helpText.text = "";
        }
    }
}
