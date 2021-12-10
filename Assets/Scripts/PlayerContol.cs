using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContol : MonoBehaviour
{
    public float speed_move;  //Скорость перемещение
    public GameObject Shoot;  //Объект для выстрела
    public float speed_shoot;  //Скорость стрельбы
    private float LastTimeShoot; //Время последнего выстрела
    public float DelayShoot;  //Задержка выстрела
    private Rigidbody2D rb;  //Физика для 2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        CharacterMove();
        CharacterShoot();
    }

    private void CharacterMove() //Перемещение персонажа
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 moveInput = new Vector2(moveX, moveY);
        rb.MovePosition(rb.position + moveInput * speed_move * Time.deltaTime);
    }

    private void CharacterShoot() //Стрельба
    {
        float ShootX = Input.GetAxis("ShootHorizontal");
        float ShootY = Input.GetAxis("ShootVertical");
        float speedX, speedY;
        if (((ShootX != 0 && ShootY == 0) || (ShootX == 0 && ShootY != 0)) && Time.time > LastTimeShoot + DelayShoot)
        {
            GameObject Shoot_clone = Instantiate(Shoot, transform.position, transform.rotation) as GameObject;
            Shoot_clone.AddComponent<Rigidbody2D>().gravityScale = 0;
            if (ShootX < 0)
            {
                speedX = Mathf.Floor(ShootX) * speed_shoot;
            }
            else
            {
                speedX = Mathf.Ceil(ShootX) * speed_shoot;
            }
            if (ShootY < 0)
            {
                speedY = Mathf.Floor(ShootY) * speed_shoot;
                Shoot_clone.transform.position = new Vector2(Shoot_clone.transform.position.x, Shoot_clone.transform.position.y - 0.15f);
            }
            else
            {
                speedY = Mathf.Ceil(ShootY) * speed_shoot;
            }
            Shoot_clone.GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, speedY);
            LastTimeShoot = Time.time;
        }
    }
}


