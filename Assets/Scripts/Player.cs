using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float earthGravity = 9.8f;
    [SerializeField] float moonGravity = 1.62f;
    [SerializeField] float force;
    [SerializeField] float rotationSpeed;
    [SerializeField] string[] animStrings;
    [SerializeField] LayerMask mountains;
    [SerializeField] LayerMask platforms;
    [SerializeField] float rayRange;
    [SerializeField] float fuel;
    [SerializeField] float fuelCost;
    [SerializeField] float maxSpeed = 2;
    [SerializeField] int lostFuel;
    public Rigidbody2D thruster;
    public ParticleSystem part;

    float initialFuel;
    bool isThrusterActivated = false;
    bool isMoving = false;
    bool isAlive = false;
    float originalGravityScale = 1;
    float newGravityScale;
    float angle = 0;
    float altitude;

    Vector2 thrusterForce;
    Animator anim;
    Vector2 initialPos;

    public delegate void CameraZoom();
    public static event CameraZoom camZoom;
    public static event CameraZoom camZoomOut;

    public delegate void Landing();
    public static event Landing landed;
    public static event Landing landedx2;
    public static event Landing landedx4;
    public static event Landing landedx5;

    public delegate void End();
    public static event End die;
    public static event End outOfFuel;
    public static event End land;
    

    void Start()
    {
        isAlive = true;
        initialFuel = fuel;
        thruster = GetComponent<Rigidbody2D>();
        newGravityScale = (moonGravity * originalGravityScale) / earthGravity; //rule of three
        thruster.gravityScale = newGravityScale; // simulation of moon gravity
        anim = GetComponent<Animator>();
        initialPos = new Vector2(transform.position.x, transform.position.y);
    }
    void Update()
    {
        if (!isAlive || Time.timeScale==0)
            return;
        thrusterForce = new Vector2(0, force);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            angle -= rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if(!part.isEmitting)
                part.Play();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if (!part.isEmitting)
                part.Play();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            ActivateAnim("isVertical");
            isThrusterActivated = true;
            isMoving = true;
            if (!part.isEmitting)
                part.Play();
            fuel -= fuelCost;
        }
        else
        {
            isMoving = false;
            isThrusterActivated = false;
        }
        
        if(thruster.velocity.magnitude > maxSpeed)
        {
            thruster.velocity = Vector2.ClampMagnitude(thruster.velocity, maxSpeed);
        }
        if(thruster.velocity.magnitude < -maxSpeed)
        {
            thruster.velocity = Vector2.ClampMagnitude(thruster.velocity, -maxSpeed);
        }
        if (!isMoving)
        {
            ActivateAnim("isIdle");
            part.Stop();
        }
        if (fuel <= 0)
        {
            outOfFuel();
            fuel = initialFuel;
        }
        PlayAnimations();
    }
    void FixedUpdate()
    {
        if (!isAlive || Time.timeScale == 0)
            return;
        if (isThrusterActivated)
        {
            thruster.AddRelativeForce(thrusterForce);
        }
        if(Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, rayRange, mountains) || Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, rayRange, platforms))
        {
            camZoom();
        }
        else
        {
            camZoomOut();
        }

        //check altitude:
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 100, mountains);
        if (hit.collider != null && (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Playtform") || hit.collider.CompareTag("PlaytformX2") || hit.collider.CompareTag("PlaytformX4") || hit.collider.CompareTag("PlaytformX5")))
        {
            altitude = Mathf.Abs(hit.point.y - transform.position.y);
        }



    }
    void PlayAnimations()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ActivateAnim("isRotatingLeft");
        }
        else
        {
            isMoving = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            ActivateAnim("isRotatingRight");
        }
        else
        {
            isMoving = false;
        }
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.RightArrow))
        {
            ActivateAnim("isDiagonalRight");
        }
        else
        {
            isMoving = false;
        }
        if(Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftArrow))
        {
            ActivateAnim("isDiagonalLeft");
        }
        else
        {
            isMoving = false;
        }
        if(Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            ActivateAnim("areAllThrustersOn");
        }
        else
        {
            isMoving = false;
        }
    }
    void ActivateAnim(string name)
    {
        for (int i = 0; i < animStrings.Length; i++)
        {
            if(animStrings[i] == name)
            {
                anim.SetBool(name, true);
            }
            else
            {
                anim.SetBool(animStrings[i], false);
            }
        }
    }

    //Getters
    public float GetFuel()
    {
        return fuel;
    }

    public Rigidbody2D GetRigidbody()
    {
        return thruster;
    }

    public float GetAltitude()
    {
        return altitude;
    }

    public bool GetAlive()
    {
        return isAlive;
    }
    public void SetAlive(bool a)
    {
        isAlive = a;
    }

    public int GetLostFuel()
    {
        return lostFuel;
    }

    //Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if(landed!=null)
                landed();
            isAlive = false;
            if(land!=null)
                land();
        }
        if (collision.gameObject.CompareTag("PlatformX2"))
        {
            if (landedx2 != null)
                landedx2();
            isAlive = false;
            if (land != null)
                land();
        }
        if (collision.gameObject.CompareTag("PlatformX4"))
        {
            if (landedx4 != null)
                landedx4();
            isAlive = false;
            if (land != null)
                land();
        }
        if (collision.gameObject.CompareTag("PlatformX5"))
        {
            if(landedx5 != null)
                landedx5();
            isAlive = false;
            if (land != null)
                land();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isAlive = false;
            fuel -= lostFuel;
            if(die!=null)
                die();
        }
    }

    //External functions
    public void Respawn()
    {
        transform.position = new Vector3(initialPos.x, initialPos.y, 0);
        isAlive = true;
        isMoving = false;
        thruster.velocity = Vector3.zero;
        thruster.angularVelocity = 0;
        thruster.angularDrag = 0.05f;
        transform.rotation = Quaternion.identity;
        angle = 0;
    }
}
