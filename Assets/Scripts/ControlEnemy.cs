using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemy : MonoBehaviour
{
    public DateEnemy[] ViewEnemy;
    private DateEnemy Enemy;
    private Transform playertrans;
    public GameObject Shoot;  //Объект для выстрела
    public float speed_shoot;  //Скорость стрельбы
    private float LastTimeShoot; //Время последнего выстрела
    public float DelayShoot;  //Задержка выстрела
    
    
    void Start()
    {
        Enemy = Instantiate(ViewEnemy[Random.Range(0, ViewEnemy.Length)]);
        Enemy.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
        playertrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    
    void Update()
    {
        if (Enemy != null)
        {
            /*Debug.Log(Enemy.CompareTag("Boss"));
            Debug.Log(Time.time > LastTimeShoot + DelayShoot);*/
            //Debug.Log(Enemy.CompareTag("Boss") /*&& Time.time > LastTimeShoot + DelayShoot*/);
            if (Enemy.CompareTag("Boss") && Time.time < LastTimeShoot + DelayShoot)
            {
                if (GameObject.FindWithTag("Player") != null)
                {
                    Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position, playertrans.position,
                        Enemy.Speed * Time.deltaTime); //Преследование игрока
                }
                else
                {
                    Enemy.transform.position = new Vector2(Enemy.transform.position.x, Enemy.transform.position.y);
                }
            }
            else if (Enemy.CompareTag("Enemy"))
            {
                if (GameObject.FindWithTag("Player") != null)
                {
                    Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position, playertrans.position,
                        Enemy.Speed * Time.deltaTime); //Преследование игрока
                }
                else
                {
                    Enemy.transform.position = new Vector2(Enemy.transform.position.x, Enemy.transform.position.y);
                }
    
                if (Enemy.Shoot && GameObject.FindWithTag("Player") != null)
                {
                    CharacterShoot();
                }
            }
            else
            {
                if (Enemy.Shoot && GameObject.FindWithTag("Player") != null)
                {
                    CharacterShoot();
                }
            }
        }
    }
    
    private void CharacterShoot() //Стрельба
    {
        float EnemyX = (float)(Enemy.transform.position.x - (Enemy.transform.position.x % 0.01));
        float EnemyY = (float)(Enemy.transform.position.y - (Enemy.transform.position.y % 0.01));
        float PlayerX = (float)(playertrans.position.x - (playertrans.position.x  % 0.01));
        float PlayerY = (float)(playertrans.position.y - (playertrans.position.y % 0.01));
        float speedX = 0, speedY = 0;
        if (((EnemyX == PlayerX) || (EnemyY == PlayerY)) && (Enemy.CompareTag("Enemy")) && Time.time > LastTimeShoot + DelayShoot)
        {
            GameObject Shoot_clone = Instantiate(Shoot, Enemy.transform.position, Enemy.transform.rotation) as GameObject;
            Shoot_clone.AddComponent<Rigidbody2D>().gravityScale = 0;
            if (PlayerX < EnemyX)
            {
                speedX = Mathf.Floor(-1) * speed_shoot;
            }
            else if (PlayerX > EnemyX)
            {
                speedX = Mathf.Ceil(1) * speed_shoot;
            }
            else if (PlayerY < EnemyY)
            {
                speedY = Mathf.Floor(-1) * speed_shoot;
                Shoot_clone.transform.position = new Vector2(Shoot_clone.transform.position.x, Shoot_clone.transform.position.y - 0.1f);
            }
            else if (PlayerY > EnemyY)
            {
                speedY = Mathf.Ceil(1) * speed_shoot;
            }
            Shoot_clone.GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, speedY);
            LastTimeShoot = Time.time;
        }
        else if (Enemy.CompareTag("Boss") && Time.time > LastTimeShoot + DelayShoot)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject clone = Instantiate(Shoot, Enemy.transform.position, Enemy.transform.rotation) as GameObject;
                clone.AddComponent<Rigidbody2D>().gravityScale = 0;
                switch (i)
                {
                    case 0:
                        speedX = 0;
                        speedY = Mathf.Ceil(1) * speed_shoot;
                        break;
                    case 1:
                        speedX = Mathf.Ceil(1) * speed_shoot;
                        speedY = Mathf.Ceil(1) * speed_shoot;
                        break;
                    case 2:
                        speedX = Mathf.Ceil(1) * speed_shoot;
                        speedY = 0;
                        break;
                    case 3:
                        speedX = Mathf.Ceil(1) * speed_shoot;
                        speedY = Mathf.Floor(-1) * speed_shoot;
                        break;
                    case 4:
                        speedX = 0;
                        speedY = Mathf.Floor(-1) * speed_shoot;
                        break;
                    case 5:
                        speedX = Mathf.Floor(-1) * speed_shoot;
                        speedY = Mathf.Floor(-1) * speed_shoot;
                        break;
                    case 6:
                        speedX = Mathf.Floor(-1) * speed_shoot;
                        speedY = 0;
                        break;
                    case 7:
                        speedX = Mathf.Floor(-1) * speed_shoot;
                        speedY = Mathf.Ceil(1) * speed_shoot;
                        break;
                }
                //Debug.Log("FUCK" + i);
                //Debug.Log(clone.name);
                clone.GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, speedY);
            }
            LastTimeShoot = Time.time;
        }
    }
}
