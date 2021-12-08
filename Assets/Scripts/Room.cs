using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject DoorUp;
    public GameObject DoorRight;
    public GameObject DoorDown;
    public GameObject DoorLeft;
    public GameObject DoorBossUp;
    public GameObject DoorBossRight;
    public GameObject DoorBossDown;
    public GameObject DoorBossLeft;
    public bool DoorBoss;
    public GameObject ColliderUp;
    public GameObject ColliderDown;
    public GameObject ColliderLeft;
    public GameObject ColliderRight;
    void Start()
    {

    }
    void Update()
    {
        
    }

    public void DeleteRoom()
    {
        Destroy(gameObject);  //удаление объекта
    }
}
