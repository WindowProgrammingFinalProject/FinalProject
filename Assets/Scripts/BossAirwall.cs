using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAirwall : MonoBehaviour
{
    AudioSource bg;
    public GameObject Bossairwall;

    private bool everentertheroom = false;//has entered the room or not
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "maincharacter" && !everentertheroom)
        {
            Debug.Log("hit");
            everentertheroom = true;//has entered thr room
            bg = GameObject.Find("maincharacter").GetComponent<AudioSource>();
            bg.Stop();
            bg = Bossairwall.GetComponent<AudioSource>();
            bg.Play();


        }//judge who enter the room and has entered the room or not
    }//enter the room



}
