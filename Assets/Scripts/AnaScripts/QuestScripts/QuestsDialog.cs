using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class QuestsDialog : MonoBehaviour
{
    [TextArea(3,10)]
    public string[] sentences;
    public TextMeshProUGUI Text;

    private int index=1;
    // Start is called before the first frame update
    void Start()
    {
        //if (sentences[0] != null)
        //{
        //    Text.text = sentences[0];
        //}
    }


    public void NextSentence()
    {
        if (sentences[index] != null&&index<sentences.Length)
        {
            Text.text = sentences[0];
            index++;
        }
    }

    public void FreeSentence()
    {
        Text.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
