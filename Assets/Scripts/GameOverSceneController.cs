using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneController : MonoBehaviour
{
    
    public void ExitButton()
    {
        Application.Quit();
    }

    public void RestartButton()
    {
        Debug.Log("restart");
        SceneManager.LoadScene("Level1Scene");
    }

}
