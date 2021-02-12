using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captured : MonoBehaviour
{
    private bool captured=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IsCaptured()
    {
        captured = true;
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[0].enabled = false;
        }
    }
    public void IsReleased()
    {
        captured = false;
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[0].enabled = true;
        }
    }
}
