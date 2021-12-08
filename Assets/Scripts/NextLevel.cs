using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public Room[] rooms;
    public GameObject Player;
    private UnityEngine.Camera camera;
    void Start()
    {
        camera = Camera.main.GetComponent<Camera>();
    }


    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector2(0, 0);
            camera.transform.position = new Vector2(0, 0);
            foreach (Room room in rooms)
            {
                room.DeleteRoom();
                yield return new WaitForSeconds(1);
            }
        }
    }
}
