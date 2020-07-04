using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


public class PlayerControl : MonoBehaviour
{    
    // Use this for initialization
    public float force;
    public float torque;
    public float Activeforce = 0.0f;
    public float Activetorque = 0.0f;
    public float timeRemaining;
    public Text FuelText;
    public Text ScoreText;
    public Text TimeText;
    public Text VelocityText;
    public Text LevelText;
    public float Burner1 = 2.0f;
    public float Burner2 = 1.5f;
    public float Burner3 = 1.5f;

    private int score;
    private Transform Co;    

    public ParticleSystem ps;
    public ParticleSystem ps2;

    public bool AndroidPushed;





    public AudioSource RocketAudio;
    public AudioSource ThrustAudio;
    public Sprite Lander_0_Sprite;
    public Sprite Lander_1_Sprite;
    public Sprite Lander_2_Sprite;
    public Sprite Lander_3_Sprite;
    public Sprite Lander_4_Sprite;
    public Sprite Lander_5_Sprite;
    public Sprite Lander_6_Sprite;

    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        RocketAudio = audios[0];
        ThrustAudio = audios[1];
        GlobalData.GlobalDataCarrier.LandedStatus = false;
        GlobalData.GlobalDataCarrier.LandedStatusOk = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckChrashed();
        timeRemaining -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Break"); 
        }
    }



    


void Movement()
    {
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        
        
        Activeforce = 0;
        Activetorque = 0;

        UpdateFuelText();
        UpdateScoreText();
        UpdateTimeText();
        UpdateVelocityText();
        AndroidPushed = false;

        if (GlobalData.GlobalDataCarrier.Fuel > 0)
        {
            // if (Input.GetKey(KeyCode.W) | (CrossPlatformInputManager.GetButton("Left") & CrossPlatformInputManager.GetButton("Right")))   (For Android control)
            if (Input.GetKey(KeyCode.W))
            {
                AndroidPushed = true;
                Activeforce = force;

                if (!RocketAudio.isPlaying)
                {
                    RocketAudio.Play();
                }

            }

            // if (Input.GetKey(KeyCode.A) | (CrossPlatformInputManager.GetButton("Left") & AndroidPushed == false))  (For Android control)
            if (Input.GetKey(KeyCode.A))
            {
                Activetorque = torque;

                if (!ThrustAudio.isPlaying)
                {
                    ThrustAudio.Play();
                }
            }

            // if (Input.GetKey(KeyCode.D) | (CrossPlatformInputManager.GetButton("Right") & AndroidPushed == false))  (For Android control)
            if (Input.GetKey(KeyCode.D) )
            {
                Activetorque = -torque;

                if (!ThrustAudio.isPlaying)
                {
                    ThrustAudio.Play();
                }
            }

            /*    if (CrossPlatformInputManager.GetButton("Left") & CrossPlatformInputManager.GetButton("Right"))  (For Android control)
                {                
                    Activetorque = 0;
                    Activeforce = force * 2;
                }*/

            // if ((Input.GetKey(KeyCode.A) & Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.D)) | (CrossPlatformInputManager.GetButton("Boost")))   (For Android control)
            if ((Input.GetKey(KeyCode.A) & Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.D)) )
            {
                Activetorque = 0;
                Activeforce = force * 2;
                if (!RocketAudio.isPlaying)
                {
                    RocketAudio.Play();
                }
                if (!ThrustAudio.isPlaying)
                {
                    ThrustAudio.Play();
                }

            }
            if (Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.D))
            {
                Activetorque = 0;
                Activeforce = force;
            }

            rb.AddForce(transform.up * Activeforce, ForceMode2D.Force);
            rb.AddTorque(Activetorque, ForceMode2D.Force);
            if (Activeforce < (force * 2))
            {
                if (Activeforce != 0 & Activetorque == 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = Lander_1_Sprite;
                    RocketEngineSmoke(0.4f,6.0f);
                    GlobalData.GlobalDataCarrier.Fuel -= Burner1;                    
                }
                else if (Activeforce == 0 & Activetorque > 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = Lander_3_Sprite;
                    RocketEngineSmoke(0.2f, 3.0f);
                    GlobalData.GlobalDataCarrier.Fuel -= Burner2;
                }
                else if (Activeforce == 0 & Activetorque < 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = Lander_2_Sprite;
                    RocketEngineSmoke(0.2f, 3.0f);
                    GlobalData.GlobalDataCarrier.Fuel -= Burner2;
                }
                else if (Activeforce != 0 & Activetorque > 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = Lander_4_Sprite;
                    RocketEngineSmoke(0.5f, 8.0f);
                    GlobalData.GlobalDataCarrier.Fuel -= Burner1 + Burner2;
                }
                else if (Activeforce != 0 & Activetorque < 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = Lander_5_Sprite;
                    RocketEngineSmoke(0.5f,8.0f);
                    GlobalData.GlobalDataCarrier.Fuel -= Burner1 + Burner3;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = Lander_0_Sprite;
                    RocketEngineSmoke(0.2f, 1.0f);
                }
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Lander_6_Sprite;
                RocketEngineSmoke(0.8f, 10.0f);
                GlobalData.GlobalDataCarrier.Fuel -= Burner1 + Burner2 + Burner3;
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Lander_0_Sprite;
            RocketEngineSmoke(0.1f, 1.0f);
        }
    }

    void UpdateFuelText()
    {
        FuelText.text = ("Fuel :" + Math.Round(GlobalData.GlobalDataCarrier.Fuel).ToString() + " Ltr");
    }

    void UpdateVelocityText()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        VelocityText.text = ("Velocity :" + Math.Round(Vector2.SqrMagnitude(rb.velocity)*10).ToString() + " kt");
    }

    void UpdateScoreText()
    {        
        LevelText.text = ("Level :" + GlobalData.GlobalDataCarrier.Level.ToString());
    }

    void UpdateTimeText()
    {
        TimeText.text = ("Time :" + Math.Round(timeRemaining).ToString());
    }

    void CheckChrashed()
    {
        ps2 = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        if (GlobalData.GlobalDataCarrier.isCrashed==true)
        {
            if (ps2.isStopped)
            {
                ps2.Play();
            }
            GlobalData.GlobalDataCarrier.isCrashed = false;
        }
    }

    void RocketEngineSmoke(float smokeForce, float smokeSpeed)
    {
        
        ps = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        ps.startSize = smokeForce;
        ps.startSpeed = smokeSpeed;
        if (ps.isStopped) { 
                ps.Play();
            }
            

    }
}
