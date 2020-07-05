using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGame : MonoBehaviour
{
    public Text scoreText;
    public Text fuelText;
    public Text altitudeText;
    public Text horizontalSpeedText;
    public Text VerticalSpeedText;
    public Text timerText;
    public Player player;
    public Rigidbody2D playerRB;

    float fuel;
    int score;
    float altitude;
    float horizontalSpeed;
    float verticalSpeed;
    float timer;

    GameManager manager;

    void Start()
    {
        manager = GameManager.Get();
    }
    public void OnClickPause() 
    {
        if (Time.timeScale == 1)
        {
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
            Time.timeScale = 0;
        }
    }

    void Update()
    {
 
        fuel = player.GetFuel();
        altitude = player.GetAltitude();
        score = manager.GetScore();
        verticalSpeed = Mathf.Abs(playerRB.velocity.y);
        horizontalSpeed = Mathf.Abs(playerRB.velocity.x);
        timer = manager.GetTimer();
        scoreText.text = "Score: " + score.ToString();
        fuelText.text = "Fuel: " + fuel.ToString("F2");
        altitudeText.text = "Altitude: " + altitude.ToString("F2");
        horizontalSpeedText.text = "Horizontal Speed: " + horizontalSpeed.ToString("F2");
        VerticalSpeedText.text = "Vertical Speed: " + verticalSpeed.ToString("F2");
        timerText.text = "Time: " + timer.ToString("F2");
    }
}
