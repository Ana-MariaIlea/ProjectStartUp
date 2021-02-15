using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    public GameObject Image;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.StartGame += OnGameStart;
    }

    public void OnGameStart()
    {
        Image.SetActive(false);
    }
}
