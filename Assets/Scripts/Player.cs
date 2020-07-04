using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float force;
    Rigidbody2D thruster;
    Vector2 thrusterForce;
    bool isThrusterActivated = false;
    float moonGravity = 1.62f;
    float earthGravity = 9.8f;
    float relativeMoonGravity;
    float originalGravityScale = 1;
    float newGravityScale;
    Vector2 gravity;
    void Start()
    {
        thruster = GetComponent<Rigidbody2D>();
        gravity = new Vector2(0, moonGravity);
        newGravityScale = (moonGravity * originalGravityScale) / earthGravity; //rule of three
        thruster.gravityScale = newGravityScale; // simulation of moon gravity

    }
    void Update()
    {
        thrusterForce = new Vector2(0, force);
        if (Input.GetKey(KeyCode.Space))
        {
            isThrusterActivated = true;
        }
        else
        {
            isThrusterActivated = false;
        }
    }
    void FixedUpdate()
    {
        if(isThrusterActivated)
        {
            thruster.AddForce(thrusterForce);
        }
    }
}
