using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationVolume : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<QuestCompletion>().Compltion();
        }
    }
}
