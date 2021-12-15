using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float TimeShoot;  //Продложительность выстрела

    void Start()
    {
        StartCoroutine(FinishShoot());
    }

    IEnumerator FinishShoot()
    {
        yield return new WaitForSeconds(TimeShoot);
        Destroy(gameObject);  //удаление объекта
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Thorn") && !other.CompareTag("NextDoor"))
        {
            if (this.CompareTag("PlayerDamage") && !other.CompareTag("ZonePlayer") && !other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            
            /*Debug.Log(this.CompareTag("EnemyDamage"));
            Debug.Log((!other.CompareTag("ZoneEnemy") && !other.CompareTag("Enemy")));
            Debug.Log((!other.CompareTag("ZoneBoss") && !other.CompareTag("Boss")));
            Debug.Log((!other.CompareTag("ZoneEnemy") && !other.CompareTag("Enemy")) && (!other.CompareTag("ZoneBoss") && !other.CompareTag("Boss")));
            Debug.Log(this.CompareTag("EnemyDamage") && ((!other.CompareTag("ZoneEnemy") && !other.CompareTag("Enemy")) && (!other.CompareTag("ZoneBoss") && !other.CompareTag("Boss"))));*/
            if (this.CompareTag("EnemyDamage") && !other.CompareTag("ZoneEnemy") && !other.CompareTag("Enemy") && !other.CompareTag("ZoneBoss") && !other.CompareTag("Boss"))
            {
                Destroy(gameObject);
            }
        }
    }
}
