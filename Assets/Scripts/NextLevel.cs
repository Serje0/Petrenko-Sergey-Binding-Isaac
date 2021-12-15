using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public RoomsPlacer rooms;
    private UnityEngine.Camera camera;
    //public bool DeleteRoom = true;
    public GameObject placer;
    private Slider Health;
    void Start()
    {
        camera = Camera.main.GetComponent<Camera>();
        Health = GameObject.Find("Canvas/Health").gameObject.GetComponent<Slider>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health.value += 2;
            other.transform.position = new Vector2(0, 0);
            camera.transform.position = new Vector2(0, 0);
            rooms.Delete();
            //rooms.Start();
            
            Destroy(GameObject.Find("RoomsPlacer"));
            Destroy(GameObject.Find("RoomsPlacer(clone)"));
            GameObject p = Instantiate(placer);
            p.transform.position = new Vector2(0, 0);
        }
    }
}
