using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float earthGravity = 9.8f;
    public float moonGravity = 1.62f;
    public float force;
    public float rotationSpeed;

    bool isThrusterActivated = false;
    float originalGravityScale = 1;
    float newGravityScale;
    float angle = 0;
    float maxSpeed = 1;
    Rigidbody2D thruster;
    Vector2 thrusterForce;

    void Start()
    {
        thruster = GetComponent<Rigidbody2D>();
        newGravityScale = (moonGravity * originalGravityScale) / earthGravity; //rule of three
        thruster.gravityScale = newGravityScale; // simulation of moon gravity

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
            isThrusterActivated = true;
        }
        else
        {
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
    }
    void FixedUpdate()
    {
        if (isThrusterActivated)
        {
            thruster.AddRelativeForce(thrusterForce);
        }
    }
}
