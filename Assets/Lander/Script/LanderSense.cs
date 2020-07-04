using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;




public class LanderSense : MonoBehaviour
{

    public bool Chrashed;
    public bool Landed;
    public bool Lost;
    public double LandingVelocity;
    public AudioSource ChrashSound;
    public AudioSource CrashVoice;
    public AudioSource LostVoice;
    public AudioSource Break;


    void OnTriggerEnter2D(Collider2D col)
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {  
            Chrashed = true;
            ChrashSound.Play();
            CrashVoice.Play();
            GlobalData.GlobalDataCarrier.isCrashed = true;

        }
        else if (col.gameObject.CompareTag("Platform"))
        {
            
            Landed = true;
            Chrashed = true;                
            CrashVoice.Play();
            Break.Play();
        } 
        else if (col.gameObject.CompareTag("Background"))
        {    
            Lost = true;
            LostVoice.Play();
        }
    }
}



