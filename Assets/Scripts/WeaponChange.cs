using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class WeaponChange : MonoBehaviour
{

    public GameObject sword;
    public GameObject gun_right;
    public GameObject gun_left;
    public GameObject raycast_right; ///�˷ǽu
    public GameObject raycast_left;

    private float lastTime;   //�p�ɾ�
    private float curTime;

    private Animator myAnimator;  //�ʵe����

    public bool now_is_sword = true;


    public int swordDamage = 15;
    public float swordAttackRange = 1.5f;

    public int gunDamage = 15;
    private bool guncanshot = true;

    public AudioClip shot;
    AudioSource audiosource;

    void Start()
    {
        myAnimator = GetComponent<Animator>(); // Animator
        gun_right.SetActive(false);
        gun_left.SetActive(false);
        raycast_right.SetActive(false);     /////���]�������
        raycast_left.SetActive(false);
        audiosource = GetComponent<AudioSource>();
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
            gun_right.SetActive(true);
            gun_left.SetActive(true);
            raycast_right.SetActive(false);     /////���]�������
            raycast_left.SetActive(false);
            now_is_sword = false;
        }
        else if (Input.GetKeyDown(KeyCode.R) && !now_is_sword)
        {
            gun_right.SetActive(false);
            gun_left.SetActive(false);
            raycast_right.SetActive(false);     /////���]�������
            raycast_left.SetActive(false);
            now_is_sword = true;
        }
        if (Input.GetMouseButtonDown(0) && !now_is_sword && guncanshot)
        {
            audiosource.PlayOneShot(shot);
            raycast_right.SetActive(true);     /////���]�������
            raycast_left.SetActive(true);
            lastTime = Time.time;      //�o�̧Q��start�}�l�ɶ}�l�p��
            guncanshot = false;
        }
        if (curTime - lastTime >= 0.5 && !guncanshot)   //�ɶ��t�j��0.5���L��
        {
            raycast_right.SetActive(false);    /////���]�������
            raycast_left.SetActive(false);
            guncanshot = true;
        }
    }
}