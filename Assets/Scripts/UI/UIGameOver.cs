using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    GameManager manager;
    public Text score;
    bool enableText;
    public GameObject txt;
    public GameObject inputField;
    private void Start()
    {
        manager = GameManager.Get();
        inputField.SetActive(false);
    }
    private void Update()
    {
        score.text = "Score: " + manager.GetScore();
        Debug.Log(name);
    }
    public void ClickOnMenu()
    {
        Time.timeScale = 1;
        manager.ResetValues();
        SceneManager.UnloadSceneAsync("GameOver");
        SceneManager.LoadScene("Menu");
    }
    public void ClickOnSave()
    {
        SaveManager.Save(name, manager.GetScore());
    }
    public void ClickOnQuit()
    {
        Application.Quit();
    }
    private void OnGUI()
    {
        if(GUILayout.Button("Enter your name..."))
        {
            enableText = !enableText;
        }
        if (enableText)
        {
            inputField.SetActive(true);
            InputField inputFieldCo = inputField.GetComponent<InputField>();
            txt.GetComponent<Text>().text = inputFieldCo.text;
            name = inputFieldCo.text;

            manager.SetName(name);
        }
    }
}
