using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationVolume : MonoBehaviour
{
    private void OnTriggerStay(Collider collision)
    {
        // Debug.Log("collision ");
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("collision with player");
            if (GetComponentInParent<QuestCompletion>() != null)
            {
                GetComponentInParent<QuestCompletion>().Compltion();
                if (GetComponentInParent<QuestCompletion>().getStatus() == QuestEvent.EventStatus.DONE)
                {
                    if (GetComponentInParent<GameEnd>()) GetComponentInParent<GameEnd>().GameEnding();
                }
            }
            
        }
    }
}
