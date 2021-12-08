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
    void Update()
    {
        
    }

    IEnumerator FinishShoot()
    {
        yield return new WaitForSeconds(TimeShoot);
        Destroy(gameObject);  //удаление объекта
    }
}
