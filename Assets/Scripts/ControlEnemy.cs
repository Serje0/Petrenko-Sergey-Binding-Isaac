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
        Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position,  playertrans.position, Enemy.Speed * Time.deltaTime);  //Преследование игрока
        if (Enemy.Shoot)
        {
            CharacterShoot();
        }
    }
    
    private void CharacterShoot() //Стрельба
    {
        float EnemyX = (float)(Enemy.transform.position.x - (Enemy.transform.position.x % 0.01));
        float EnemyY = (float)(Enemy.transform.position.y - (Enemy.transform.position.y % 0.01));
        float PlayerX = (float)(playertrans.position.x - (playertrans.position.x  % 0.01));
        float PlayerY = (float)(playertrans.position.y - (playertrans.position.y % 0.01));
        if (((EnemyX == PlayerX) || (EnemyY == PlayerY)) && Time.time > LastTimeShoot + DelayShoot)
        {
            float speedX = 0, speedY = 0;
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
                Shoot_clone.transform.position = new Vector2(Shoot_clone.transform.position.x, Shoot_clone.transform.position.y - 0.15f);
            }
            else if (PlayerY > EnemyY)
            {
                speedY = Mathf.Ceil(1) * speed_shoot;
            }
            Shoot_clone.GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, speedY);
            LastTimeShoot = Time.time;
        }
    }
}
