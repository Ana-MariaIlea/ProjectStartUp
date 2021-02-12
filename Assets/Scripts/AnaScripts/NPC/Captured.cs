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
    }
    public void IsReleased()
    {
        captured = false;
    }
}
