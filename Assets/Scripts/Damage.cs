using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private Slider Health;
    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health = GameObject.Find("Canvas/Health").gameObject.GetComponent<Slider>();
            Health.value -= 1;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
