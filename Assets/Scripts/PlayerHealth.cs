using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Slider Health;
    public GameObject dead;
    private float LastTime; //Время последнего урона

    private IEnumerator OnTriggerEnter2D (Collider2D other)
    {
        Health = GameObject.Find("Canvas/Health").gameObject.GetComponent<Slider>();
        if (GameObject.FindWithTag("EnemyDamage") != null)
        {
            if (other.CompareTag("EnemyDamage"))
            {
                if (Health.value > 0.25f)
                {
                    Health.value -= 0.25f;
                    yield return new WaitForSeconds(1.0f);
                }
                else
                {
                    Delete();
                    yield return new WaitForSeconds(1.0f);
                }
            }
        }
        
        if (GameObject.FindWithTag("ZoneEnemy") != null && (Time.time > LastTime + 20f))
        {
            if (other.CompareTag("ZoneEnemy"))
            {
                if (Health.value > 0.5f)
                {
                    Health.value -= 0.5f;
                    yield return new WaitForSeconds(1.0f);
                }
                else
                {
                    Delete();
                    yield return new WaitForSeconds(1.0f);
                }

                LastTime = Time.time;
            }
        }
    }

    void Delete()
    {
        Health.value = 0;
        Destroy(GameObject.FindWithTag("Player"));
        dead.SetActive(true);
                
        int bestcheck = PlayerPrefs.GetInt("BestCheck");
        int check = PlayerPrefs.GetInt("Check");
        if (bestcheck < check)
        {
            PlayerPrefs.GetInt("BestCheck", check);
            PlayerPrefs.Save();
        }
    }
}
