using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
       // Debug.Log("collision ");
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("collision with player");
            GetComponentInParent<QuestCompletion>().Compltion();
        }
    }
}
