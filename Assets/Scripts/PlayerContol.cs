using System;
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
            GameObject Shoot_dub = Instantiate(Shoot, transform.position, transform.rotation);
            Shoot_dub.AddComponent<Rigidbody2D>().gravityScale = 0;
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
            }
            else
            {
                speedY = Mathf.Ceil(ShootY) * speed_shoot;
            }
            Shoot_dub.GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, speedY);
            LastTimeShoot = Time.time;
        }
    }
}


