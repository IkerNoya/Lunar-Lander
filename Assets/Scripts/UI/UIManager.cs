using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    GameManager Manager;
    private void Start()
    {
        Manager = GameManager.Get();
    }
    public void ClickOnPlay()
    {
        SceneManager.LoadScene("Loading Level");
        Manager.SelectLevel();
    }
    public void ClickOnCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void ClickOnQuit()
    {
        Application.Quit();
    }
    public void ClickOnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ClickOnScores()
    {
        SceneManager.LoadScene("Highscores");
    }
}
