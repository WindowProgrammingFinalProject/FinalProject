using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonController : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("StoreScene");
    }

    public void MaxHealthButton()
    {

    }
    public void MaxShieldButton()
    {

    }
    public void MaxSpeedButton()
    {

    }
    public void HealUpButton()
    {

    }
}
