using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneLoaderScript : MonoBehaviour
{
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private Slider loadingBar;

    public void OnLoadScene(int level)
    {
        LevelDecider.Level = level;
        StartCoroutine(StartLoading(SceneNames.MeteorSmash.GAME_THROW_SCENE));
    }

    private IEnumerator StartLoading(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            this.loadingBar.value = progress;
            yield return null;
        }
    }
}
