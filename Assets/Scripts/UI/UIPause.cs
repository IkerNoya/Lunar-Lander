using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIPause : MonoBehaviour
{
    public void ClickOnContinue()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("Pause");
    }

}
