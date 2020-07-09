using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
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
    string name;

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
    public void SetName(string user)
    {
        name = user;
    }
    public string GetName()
    {
        return name;
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
        Player.land += Land;
        LevelLoader.loaded += FindPlayer;
    }

    private void Update()
    {
        if (player != null && !player.GetAlive())
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
        SceneManager.LoadScene("Crashed", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }
    void OutOfFuel()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }
    void Land()
    {
        SceneManager.LoadScene("Landed",LoadSceneMode.Additive);
        Time.timeScale = 0;
    }
    void FindPlayer()
    {
        StartCoroutine(FindP());
    }
    IEnumerator FindP()
    {
        yield return new WaitForSeconds(0.2f);
        player = FindObjectOfType<Player>();
        StopCoroutine(FindP());
        yield return null;
    }
    public float RestartTimer(float t)
    {
        t = 0;
        return t;
    }
    public int SelectLevel()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        levelChoice = Random.Range(0, 3);
        if (levelChoice == 3) levelChoice = 2;
        currentLevelSelection = levelChoice;
        return levelChoice;
    }
    public void ResetValues()
    {
        SelectLevel();
        player.Respawn();
        timer = 0;
        RestartTimer(timer);
    }
    public void StartTimer()
    {
        if(player!=null)
            player.SetAlive(true);
        RestartTimer(timer);
    }
    private void OnDisable()
    {
        Player.landed -= AddScore;
        Player.landedx2 -= AddScoreX2;
        Player.landedx4 -= AddScoreX4;
        Player.landedx5 -= AddScoreX5;
        Player.die -= Die;
        Player.outOfFuel -= OutOfFuel;
        Player.land -= Land;
        LevelLoader.loaded -= FindPlayer;
    }
}
