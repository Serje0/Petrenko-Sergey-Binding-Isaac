using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float TimeShoot;  //Продложительность выстрела

    void Start()
    {
        //условия расположение объекта, т.е. не удалять первоначального объекта
        if (gameObject.transform.position.x != 0 || gameObject.transform.position.y != 0 || gameObject.transform.position.z != 1)  
        {
            StartCoroutine(FinishShoot());
        }
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
