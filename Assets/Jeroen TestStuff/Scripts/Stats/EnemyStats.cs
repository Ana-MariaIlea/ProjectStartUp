using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    public override void handleDeath()
    {
        Debug.Log(transform.name + " died");
        Destroy(gameObject);
    }
}
