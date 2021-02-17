using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
    public TextMeshProUGUI text;

    int fadeCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fadeAway(float alphaLeft)
    {
        Color c = text.color;
        c.a = alphaLeft;
    }
    void fadeIn(float alphaLeft)
    {
        Color c = text.color;
        c.a = alphaLeft;
    }
}
