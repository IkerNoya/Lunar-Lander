using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICrash : MonoBehaviour
{
    GameManager manager;
    public Player player;
    public Text lostFuel;
    private void Start()
    {
        manager = GameManager.Get();
    }
    private void Update()
    {
        lostFuel.text = "Lost fuel: " + player.GetLostFuel().ToString();
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Time.timeScale = 1;
            manager.ResetValues();
            SceneManager.UnloadSceneAsync("Crashed");
        }
    }
}
