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
            if (Health.value > 0.5f)
            {
                Health = GameObject.Find("Canvas/Health").gameObject.GetComponent<Slider>();
                Health.value -= 0.5f;
                yield return new WaitForSeconds(1);
            }
            else
            {
                Health.value = 0;
                Destroy(GameObject.FindWithTag("Player"));
                GameObject.FindWithTag("Dead").SetActive(true);
                yield return new WaitForSeconds(1);
            }
        }
    }
}
