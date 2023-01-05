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
        Debug.Log("go to store");
        SceneManager.LoadScene("StoreScene");
    }

}
