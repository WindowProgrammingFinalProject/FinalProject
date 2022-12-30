using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreControllerScript : MonoBehaviour
{

    public int currentCoin;

    void Start()
    {
        currentCoin = PlayerPrefs.GetInt("coin");
    }

}
