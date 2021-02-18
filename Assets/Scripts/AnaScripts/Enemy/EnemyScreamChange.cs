using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScreamChange : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ChangeState()
    {
        anim.SetInteger("condition", 4);
        anim.SetInteger("scream", 1);
    }
}
