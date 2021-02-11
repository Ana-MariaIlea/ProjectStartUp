using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTakeDamage : MonoBehaviour,ITakeDamage
{

    float health = 10;
    public void TakeDamage()
    {
        health--;
        Debug.Log(health);
    }


}
