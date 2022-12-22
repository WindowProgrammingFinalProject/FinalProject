using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject sword;
    public GameObject gun;
    public GameObject raycast;

    public bool now_is_sword = true;

    void Start()
    {
        gun.SetActive(false);
        raycast.SetActive(false);     /////���]�������
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && now_is_sword)   ///////////���~���ҥλP����
        {                                                  ///////////https://www.cg.com.tw/UnityCSharp/Content/SetActive.php
            sword.SetActive(false);
            gun.SetActive(true);
            raycast.SetActive(true);
            now_is_sword = false;
        }
        else if(Input.GetKeyDown(KeyCode.R) && !now_is_sword)
        {
            sword.SetActive(true);
            gun.SetActive(false);
            raycast.SetActive(false);
            now_is_sword = true;
        }
    }
}
