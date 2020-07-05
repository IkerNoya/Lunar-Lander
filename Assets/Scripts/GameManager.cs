using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int currentLevelSelection = 100;
    public int levelChoice = 100;
    int score = 0;
    public static GameManager Get()
    {
        return instance;
    }
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Player.landed += AddScore;
        Player.landedx2 += AddScoreX2;
        Player.landedx4 += AddScoreX4;
        Player.landedx5 += AddScoreX5;
    }
    void AddScore()
    {
        score += 50;
    }
    void AddScoreX2()
    {
        score += 100;
    }
    void AddScoreX4()
    {
        score += 200;
    }
    void AddScoreX5()
    {
        score += 250;
    }
    public int SelectLevel()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        levelChoice = Random.Range(0, 3);
        if (levelChoice == 3) levelChoice = 2;
        currentLevelSelection = levelChoice;
        return levelChoice;
    }
    private void OnDisable()
    {
        Player.landed -= AddScore;
        Player.landedx2 -= AddScoreX2;
        Player.landedx4 -= AddScoreX4;
        Player.landedx5 -= AddScoreX5;
    }
}
