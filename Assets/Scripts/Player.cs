using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float earthGravity = 9.8f;
    public float moonGravity = 1.62f;
    public float force;
    public float rotationSpeed;
    public string[] animStrings;

    bool isThrusterActivated = false;
    bool isMoving = false;
    float originalGravityScale = 1;
    float newGravityScale;
    float angle = 0;
    float maxSpeed = 1;

    Rigidbody2D thruster;
    Vector2 thrusterForce;
    Animator anim;

    void Start()
    {
        thruster = GetComponent<Rigidbody2D>();
        newGravityScale = (moonGravity * originalGravityScale) / earthGravity; //rule of three
        thruster.gravityScale = newGravityScale; // simulation of moon gravity
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        thrusterForce = new Vector2(0, force);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            angle -= rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        if (Input.GetKey(KeyCode.Space))
        {
            ActivateAnim("isVertical");
            isThrusterActivated = true;
            isMoving = true;
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
        if(!isMoving)
        {
            ActivateAnim("isIdle");
        }
        PlayAnimations();
    }
    void FixedUpdate()
    {
        if (isThrusterActivated)
        {
            thruster.AddRelativeForce(thrusterForce);
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
}
