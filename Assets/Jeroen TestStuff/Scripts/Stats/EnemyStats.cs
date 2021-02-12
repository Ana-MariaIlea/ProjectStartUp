using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    public override void handleDeath()
    {
        Debug.Log(transform.name + " died");

        // Change it so that enemy stops moving but the corpse is still in the game 
        //GetComponent<EnemyBehaviour>().EnemyDies();
       // GetComponent<EnemyBehaviour>().enabled = false;
       /// this.enabled = false;
        Destroy(gameObject);
    }
}
