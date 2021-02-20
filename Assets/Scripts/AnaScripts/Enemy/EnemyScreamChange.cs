using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScreamChange : MonoBehaviour
{
    Animator anim;

    private Transform target;
    int damageDone=0;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ChangeState()
    {
        if (anim != null)
        {
            anim.SetInteger("condition", 4);
            anim.SetInteger("scream", 1);
        }
    }


    public void SetParamForAttack(Transform t, int damage)
    {
        target = t;
        damageDone = damage;
    }
    public void ActualAttack()
    {
        Debug.Log("Attack");
        if (target != null)
        {
            if (target.GetComponent<CharacterStats>() != null) target.GetComponent<CharacterStats>().TakeDamage(damageDone);
        }
    }
}
