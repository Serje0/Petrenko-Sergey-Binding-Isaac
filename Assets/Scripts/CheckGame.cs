using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckGame : MonoBehaviour
{
    public Text Text1;
    public Text Text2;
    public Text Text3;
    void Start()
    {
        if (Text1 != null)
        {
            int bestCheck = PlayerPrefs.GetInt("BestCheck");
            Text1.text = "Рекорд счет:\n" + bestCheck;
        }
        if (Text2 != null)
        {
            int check = PlayerPrefs.GetInt("Check");
            Text2.text = "Предыдущий счет:\n" + check;
        }
        if (Text3 != null)
        {
            int check = PlayerPrefs.GetInt("Check");
            Text3.text = "Ваш счет:\n" + check;
        }
    }
}
