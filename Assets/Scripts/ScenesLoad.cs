using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoad : MonoBehaviour
{
    public void SceneLoad(int index)
    {
        SceneManager.LoadScene(index);
    }
    
    public void SceneExit(GameObject ExitUI)
    {
        ExitUI.SetActive(true);
    }
    
    public void ExitYes()
    {
        Application.Quit();
    }
    
    public void ExitNo(GameObject ExitUI)
    {
        ExitUI.SetActive(false);
    }
}
