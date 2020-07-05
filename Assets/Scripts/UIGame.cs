﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    int fuel;
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

    void Update()
    {
 
        fuel = (int)player.GetFuel();
        altitude = player.GetAltitude();
        score = manager.GetScore();
        verticalSpeed = Mathf.Abs(playerRB.velocity.y);
        horizontalSpeed = Mathf.Abs(playerRB.velocity.x);
        timer = manager.GetTimer();
        scoreText.text = "Score: " + score.ToString();
        fuelText.text = "Fuel: " + fuel.ToString();
        altitudeText.text = "Altitude: " + altitude.ToString("F2");
        horizontalSpeedText.text = "Horizontal Speed: " + horizontalSpeed.ToString("F2");
        VerticalSpeedText.text = "Vertical Speed: " + verticalSpeed.ToString("F2");
        timerText.text = "Time: " + timer.ToString("F2");
    }
}
