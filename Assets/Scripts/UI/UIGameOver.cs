using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    GameManager manager;
    private void Start()
    {
        manager = GameManager.Get();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Time.timeScale = 1;
            manager.ResetValues();
            SceneManager.UnloadSceneAsync("GameOver");
            SceneManager.LoadScene("Menu");
        }
    }
}
