using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    // public GameObject Image;
    public GameObject[] obejectsToEnable;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.StartGame += OnGameStart;
    }

    public void OnGameStart()
    {
        for (int i = 0; i < obejectsToEnable.Length; i++)
        {
            obejectsToEnable[i].SetActive(true);
        }
       // Image.SetActive(false);
    }
}
