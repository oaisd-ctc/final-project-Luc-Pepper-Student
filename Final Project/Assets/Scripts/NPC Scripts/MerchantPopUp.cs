using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MerchantPopUp : MonoBehaviour
{
    public void OnClickContinue()
    {
        SceneManager.LoadScene(5);
    }
    public void OnClickExit()
    {
        SceneManager.LoadScene(0);
    }
}
