using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void ClickOnPlay()
    {
        SceneManager.LoadScene("Game");
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
}
