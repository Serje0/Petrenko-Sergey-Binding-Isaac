using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
        public DateEnemy Enemy;
        private IEnumerator OnTriggerEnter2D (Collider2D other)
        {
                if (other.CompareTag("PlayerDamage"))
                {
                        if (Enemy.Health > 0.25f)
                        {
                                Enemy.Health -= 0.25f;
                                yield return new WaitForSeconds(1.0f);
                        }
                        else
                        {
                            Destroy(this.transform.parent.gameObject);
                            /*int check = PlayerPrefs.GetInt("Check");
                            //Debug.Log(check);
                            check += 10;*/
                            //Debug.Log(check);
                            PlayerPrefs.GetInt("Check", PlayerPrefs.GetInt("Check") + 10);
                            Debug.Log(PlayerPrefs.GetInt("Check"));
                            //PlayerPrefs.Save();
                            yield return new WaitForSeconds(1.0f);
                        }
                }
        }
}

