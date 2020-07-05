using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Player player;

    public int currentLevelSelection = 100;
    public int levelChoice = 100;
    int score = 0;
    float timer;

    public static GameManager Get()
    {
        return instance;
    }
    public int GetScore()
    {
        return score;
    }
    public float GetTimer()
    {
        return timer;
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
        Player.die += Die;
        Player.outOfFuel += OutOfFuel;
    }

    private void Update()
    {
        if (!player.GetAlive())
            return;
        timer += Time.deltaTime;
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
    void Die()
    {

    }
    void OutOfFuel()
    {

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
