using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject loadingScreen;
    GameManager manager;
    float loadingProgress;
    float timeLoading;
    public delegate void LoadedLevel();
    public static event LoadedLevel loaded;
    private void Start()
    {
        StartCoroutine(LoadAsyncOperation());
        manager = GameManager.Get();
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
                manager.StartTimer();
                level.allowSceneActivation = true;
                if (loaded != null)
                    loaded();
            }
            yield return null;
        }
    }
    
}
