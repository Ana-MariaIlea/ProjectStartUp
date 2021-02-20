using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameEnd : MonoBehaviour
{
    public GameObject[] objectsToTurnOff;
    public GameObject[] objectsToTurnOn;
    public PlayableDirector director;


    public void GameEnding()
    {
        for (int i = 0; i < objectsToTurnOff.Length; i++)
        {
            objectsToTurnOff[i].SetActive(false);
        }

        for (int i = 0; i < objectsToTurnOn.Length; i++)
        {
            objectsToTurnOn[i].SetActive(true);
        }

        director.Play();
    }

    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
            Application.Quit();
    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }
}
