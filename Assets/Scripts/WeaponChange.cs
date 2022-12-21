using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject sword;
    public GameObject gun;
    public GameObject raycast; ///瞄準線
    public GameObject laser;   ///雷射光

    private float lastTime;   //計時器
    private float curTime;

    private Animator myAnimator;  //動畫控制

    private bool now_is_sword = true;

    void Start()
    {
        myAnimator = GetComponent<Animator>(); // Animator
        gun.SetActive(false);
        raycast.SetActive(false);     /////先設為不顯示
        laser.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        curTime = Time.time;  ///計時，現在時間
        if (Input.GetKeyDown(KeyCode.R) && now_is_sword)   ///////////物品的啟用與停用
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
        if (Input.GetMouseButtonDown(0) && !now_is_sword)
        {
           // myAnimator.SetBool()
            laser.SetActive(true);
            lastTime = Time.time;      //這裡利用start開始時開始計時
        }
        if (curTime - lastTime >= 0.5)   //時間差大於0.5秒過後
        {
            laser.SetActive(false);
        }
    }
}
