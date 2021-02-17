using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
   // public GameObject Image;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.StartGame += OnGameStart;
    }

    public void OnGameStart()
    {
        Player.SetActive(true);
       // Image.SetActive(false);
    }
}
