using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;

public class GameThrowSceneLoaderScript : MonoBehaviour
{
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private Slider loadingBar;

    public void OnLoadScene(string sceneName)
    {
        Debug.Log("LOADING SCENE");
        StartCoroutine(StartLoading(sceneName));
    }

    private IEnumerator StartLoading(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        LoadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            this.loadingBar.value = progress;
            yield return null;
        }
    }
}
