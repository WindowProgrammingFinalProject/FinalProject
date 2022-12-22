using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject sword;
    public GameObject gun;
    public GameObject raycast; ///�˷ǽu
    public GameObject laser;   ///�p�g��

    private float lastTime;   //�p�ɾ�
    private float curTime;

    private Animator myAnimator;  //�ʵe����

    public bool now_is_sword = true;

    //[RequireComponent(typeof(AudioSource))]

    void Start()
    {
        myAnimator = GetComponent<Animator>(); // Animator
        gun.SetActive(false);
        raycast.SetActive(false);     /////���]�������
        laser.SetActive(false);
        //laser = GameObject.Find("laser");
        //audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        curTime = Time.time;  ///�p�ɡA�{�b�ɶ�
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
        if (Input.GetMouseButtonDown(0) && !now_is_sword)
        {
           // myAnimator.SetBool()
            laser.SetActive(true);
            raycast.SetActive(false);
            lastTime = Time.time;      //�o�̧Q��start�}�l�ɶ}�l�p��
        }
        if (curTime - lastTime >= 0.5)   //�ɶ��t�j��0.5���L��
        {
            laser.SetActive(false);
            raycast.SetActive(true);
        }
    }
}
