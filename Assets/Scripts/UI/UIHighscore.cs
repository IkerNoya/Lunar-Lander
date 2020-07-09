using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHighscore : MonoBehaviour
{
    GameManager manager;
    public Text newScore;
    public Text currentHighscore;
    int score;
    int highscore;
    string name;
    string highscoreName;
    public struct Highscore
    {
        public string Name;
        public int Score;
    }
    Highscore h = new Highscore();
    void Start()
    {
        manager = GameManager.Get();
        score = manager.GetScore();
        name = manager.GetName();
        SaveManager.Load(ref h.Name, ref h.Score);
        highscore = h.Score;
        highscoreName = h.Name;
    }

    // Update is called once per frame
    void Update()
    {
        newScore.text = name + ": " + score.ToString();
        currentHighscore.text = "Highscore: " + highscore.ToString() + " by " + highscoreName;
    }
}
