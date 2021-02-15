using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int titleScreenInterger;
    public int mainScreenInterger;
    public GameObject loadingScreen;
    public AudioClip intro;

    private AudioSource audio;

    public Action StartGame;

    private void Awake()
    {
        if(instance==null)
        instance = this;

        SceneManager.LoadSceneAsync(titleScreenInterger, LoadSceneMode.Additive);
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = intro;
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void LoadGame()
    {
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(titleScreenInterger));
        scenesLoading.Add(SceneManager.LoadSceneAsync(mainScreenInterger, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;

            }
        }
        loadingScreen.SetActive(false);
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        StartGame.Invoke();
        //intro.
    }
}
