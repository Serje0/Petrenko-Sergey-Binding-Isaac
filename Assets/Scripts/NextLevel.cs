using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public RoomsPlacer rooms;
    private UnityEngine.Camera camera;
    public bool DeleteRoom = true;
    void Start()
    {
        camera = Camera.main.GetComponent<Camera>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector2(0, 0);
            camera.transform.position = new Vector2(0, 0);
            rooms.Delete();
            rooms.Start();
        }
    }
}
