using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float force;
    public Rigidbody2D thrusterForce;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            //transform.position +=  * speed;
        }
    }
}
