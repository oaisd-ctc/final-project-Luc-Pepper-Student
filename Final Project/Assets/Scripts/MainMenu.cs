using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene(0);
    }
    public void OnClickOptions()
    {

    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
