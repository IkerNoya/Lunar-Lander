using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILanded : MonoBehaviour
{
    GameManager manager;
    public Player player;
    public Text score;
    private void Start()
    {
        manager = GameManager.Get();
    }
    private void Update()
    {
        score.text = manager.GetScore().ToString();
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Time.timeScale = 1;
            manager.ResetValues();
            SceneManager.UnloadSceneAsync("Landed");
        }
    }
}
