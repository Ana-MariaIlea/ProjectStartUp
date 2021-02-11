using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.transform.GetComponent<PlayerStats>().addMedKit();
            Destroy(this.gameObject);
        }
    }
}
