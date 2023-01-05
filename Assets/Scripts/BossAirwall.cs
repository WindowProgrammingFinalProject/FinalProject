using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAirwall : MonoBehaviour
{
    AudioSource bg;   //background music
    public GameObject Bossairwall;
    public GameObject Boss;
    public GameObject smal11;
    public GameObject small2;
    public GameObject small3;



    private bool everentertheroom = false;//has entered the room or not
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "maincharacter" && !everentertheroom)
        {
            Debug.Log("hit");
            everentertheroom = true;//has entered thr room
            bg = GameObject.Find("maincharacter").GetComponent<AudioSource>();
            bg.Stop();
            bg = Bossairwall.GetComponent<AudioSource>();    //get the compomenet of Bossairwall
            bg.Play();
            Boss.SetActive(true);
            smal11.SetActive(true);
            small2.SetActive(true);
            small3.SetActive(true);

        }//judge who enter the room and has entered the room or not
    }//enter the room



}
