using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform player;
    Vector3 offset;
    Vector3 initialPos;
    void Start()
    {
        Player.camZoom += FollowPlayer;
        Player.camZoomOut += ZoomOut;
        initialPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(0, 0, -5);   
    }

    void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position ,player.position + offset, 0.5f);
    }
    void ZoomOut()
    {
        transform.position = Vector3.Lerp(transform.position, initialPos, 0.5f);
    }
    private void OnDisable()
    {
        Player.camZoom -= FollowPlayer;
        Player.camZoomOut -= ZoomOut;
    }
}
