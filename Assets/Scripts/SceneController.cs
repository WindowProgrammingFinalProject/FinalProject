using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void Start()
    {
        //Invoke(nameof(GoToStore), 5);   
    }

    public void LateGoToStore()
    {
        Invoke(nameof(GoToStore), 5);
    }

    void GoToStore()
    {
        GameObject.Find("maincharacter").GetComponent<PlayerController>().GoToStoreScene();
    }

    void GoToVictoryScene()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public void LateGoToVictoryScene()
    {
        Invoke(nameof(GoToVictoryScene), 5);
    }
}
