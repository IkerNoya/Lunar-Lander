using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject loadingScreen;

    float progressAmmount;
    float loadingProgress;
    float timeLoading;
    private void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        loadingProgress = 0;
        timeLoading = 0;
        yield return null;

        AsyncOperation level = SceneManager.LoadSceneAsync("Game");
        level.allowSceneActivation = false;

        loadingScreen.SetActive(true);

        while (!level.isDone )
        {
            timeLoading += Time.deltaTime;
            loadingProgress = level.priority + 0.1f;
            loadingProgress = loadingProgress * timeLoading*2;
             slider.value = loadingProgress;
            if(loadingProgress>=1)
            {
                level.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    
}
