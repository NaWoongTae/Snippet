using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseManager : MonoBehaviour
{
    static public BaseManager instance;

    // Use this for initialization
    void Start()
    {
        instance = this;
        StartCoroutine(StartLogoScene());
    }
    // 로고 씬
    IEnumerator StartLogoScene()
    {
        AsyncOperation AO = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        while (!AO.isDone)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }

    // 씬 전환
    IEnumerator LoadingScene(string remove, int load)
    {
        yield return new WaitForSeconds(0.1f);

        AsyncOperation AO;
        if (remove != string.Empty)
        {
            AO = SceneManager.UnloadSceneAsync(remove);
            while (!AO.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        AO = SceneManager.LoadSceneAsync(load, LoadSceneMode.Additive);
        while (!AO.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));

        yield return new WaitForSeconds(0.1f);
    }

    // 씬 전환
    public void convertScene(string close, int open)
    {
        StartCoroutine(LoadingScene(close, open));
    }
}
