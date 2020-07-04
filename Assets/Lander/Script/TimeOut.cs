using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;


public class TimeOut : MonoBehaviour
{

    public GameObject Lander_0;
    public Text StatusText;
    private PlayerControl ScriptToAccess;
    private LanderSense Script2ToAccess;

    private float timeRemaining;
    private bool Chrashed;
    private bool Landed;
    private bool Lost;
    public AudioSource LandedVoice;

    // Use this for initialization
    void Start()
    {
        ScriptToAccess = Lander_0.GetComponent<PlayerControl>();
        Script2ToAccess = Lander_0.GetComponent<LanderSense>();

        Chrashed = false;
        Landed = false;
        Lost = false;

    }

    // Update is called once per frame
    void Update()
    {
        CheckTimeExec();
        CheckLandedExec();
        CheckCrashExec();
        CheckLostExec();
    }

    void CheckTimeExec()
    {
        timeRemaining = ScriptToAccess.timeRemaining;
        if (timeRemaining <= 0.1f)
        {
            StatusText.text = ("TimeOUT");
            StartCoroutine(PauseExecution(5f, 0f, true));
        }
    }



    void CheckLandedExec()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Landed = Script2ToAccess.Landed;
        Chrashed = Script2ToAccess.Chrashed;
        if (Landed & Chrashed)
        {
            StatusText.text = ("You landed too hard on the Platform");
            GlobalData.GlobalDataCarrier.LandedLeft = false;
            GlobalData.GlobalDataCarrier.LandedRight = false;
          
            StartCoroutine(PauseExecution(5f, 0.3f, true));
        }
        else if (GlobalData.GlobalDataCarrier.LandedLeft & GlobalData.GlobalDataCarrier.LandedRight)
        {

            //* Kolla hastigheten just vid det ögonblick då båda benen touchar


            if ((rb.rotation < 1.0f) & (rb.rotation >-1.0f) & ((Vector2.SqrMagnitude(rb.velocity) * 10) < 1.0f) & GlobalData.GlobalDataCarrier.LandedStatusOk == false)
            {
                LandedVoice.Play();
                GlobalData.GlobalDataCarrier.LandedStatusOk = true;
                StatusText.text = ("You landed on the Platform" + Convert.ToChar(10) + "10000 points" + Convert.ToChar(10) + "300 litres of fuel added");
                GlobalData.GlobalDataCarrier.LandedLeft = false;
                GlobalData.GlobalDataCarrier.LandedRight = false;
                StartCoroutine(PauseExecution(5f, 0.3f, false));
            }
        }

    }



    void CheckCrashExec()
    {
        Chrashed = Script2ToAccess.Chrashed;
        if (Chrashed & !Landed)
        {
            StatusText.text = ("You Crashed into the ground");
            StartCoroutine(PauseExecution(5f, 0.3f, true));
        }
    }
    void CheckLostExec()
    {
        Lost = Script2ToAccess.Lost;
        if (Lost)
        {
            StatusText.text = ("You are lost into space");
            StartCoroutine(PauseExecution(5f, 0f, true));
        }
    }



    public IEnumerator PauseExecution(float pauseTime, float slowMot, bool GameOver)
    {

        Time.timeScale = slowMot;

        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {

            yield return 0;
        }

        Time.timeScale = 1f;
        if (GameOver)
        {
            if (GlobalData.GlobalDataCarrier.HighScore < GlobalData.GlobalDataCarrier.Score)
            {
                GlobalData.GlobalDataCarrier.HighScore = GlobalData.GlobalDataCarrier.Score;
            }            
            GlobalData.GlobalDataCarrier.Level = 1;
            GlobalData.GlobalDataCarrier.LandedLeft = false;
            GlobalData.GlobalDataCarrier.LandedRight = false;
            Script2ToAccess.Landed = false;
            Script2ToAccess.Chrashed = false;
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            if (GlobalData.GlobalDataCarrier.LandedStatus == false)
            {
                GlobalData.GlobalDataCarrier.LandedStatus = true;
                GlobalData.GlobalDataCarrier.Score += 10000;
                GlobalData.GlobalDataCarrier.Fuel += 300;
                GlobalData.GlobalDataCarrier.Level += 1;
                GlobalData.GlobalDataCarrier.LandedLeft = false;
                GlobalData.GlobalDataCarrier.LandedRight = false;
                switch (GlobalData.GlobalDataCarrier.Level)
                {
                    case (1):
                        SceneManager.LoadScene("Lander1");
                        break;
                    case (2):
                        SceneManager.LoadScene("Lander2");
                        break;
                    case (3):
                        SceneManager.LoadScene("Lander3");
                        break;
                    case (4):
                        SceneManager.LoadScene("Lander4");
                        break;
                    case (5):
                        SceneManager.LoadScene("Lander5");
                        break;
                    case (6):
                        SceneManager.LoadScene("Lander6");
                        break;
                    case (7):
                        SceneManager.LoadScene("Lander7");
                        break;
                    case (8):
                        SceneManager.LoadScene("Lander8");
                        break;
                    case (9):
                        SceneManager.LoadScene("Lander9");
                        break;
                    case (10):
                        SceneManager.LoadScene("Lander10");
                        break;
                    case (11):
                        SceneManager.LoadScene("Lander6");
                        break;
                    case (12):
                        SceneManager.LoadScene("Lander6");
                        break;
                    case (13):
                        SceneManager.LoadScene("Lander7");
                        break;
                    case (14):
                        SceneManager.LoadScene("Lander7");
                        break;
                    case (15):
                        SceneManager.LoadScene("Lander8");
                        break;
                    case (16):
                        SceneManager.LoadScene("Lander8");
                        break;
                    case (17):
                        SceneManager.LoadScene("Lander9");
                        break;
                    case (18):
                        SceneManager.LoadScene("Lander9");
                        break;
                    default:
                        SceneManager.LoadScene("Lander10");
                        break;
                }

            }
        }
    }
}