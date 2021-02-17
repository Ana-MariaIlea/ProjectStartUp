using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TestTimeline : MonoBehaviour
{
    public PlayableDirector timeline;


    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            timeline.Play();
        }
    }
}
