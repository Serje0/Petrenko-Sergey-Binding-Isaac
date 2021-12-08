using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoom : MonoBehaviour
{
    public Vector3 CameraPos;
    public Vector3 PlayerPos;
    private UnityEngine.Camera camera;

    private void Start()
    {
        camera = Camera.main.GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position += PlayerPos;
            camera.transform.position += CameraPos;
        }
    }
}
