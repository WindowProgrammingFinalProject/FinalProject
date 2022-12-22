using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] gameObjects;//kinds of enemy type, now only mushroom
    private bool everentertheroom = false;//has entered the room or not
    GameObject[] enemy = new GameObject[6];//create <= 6 enemy
    int enemyNumber;//enemy number

    //when player enter the room, create 3~6 enemy
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "maincharacter" && !everentertheroom)
        {
            Debug.Log("spawn!!");

            everentertheroom = true;//has entered thr room
            enemyNumber = Random.Range(3, 7);//random enemy number

            for (int i = 0; i < enemyNumber; i++)
            {
                int randomindex = Random.Range(0, gameObjects.Length);
                Vector3 randomposition = new Vector3(Random.Range(transform.position.x - 13f, transform.position.x - 1f), -1.5f, Random.Range(transform.position.z - 7f, transform.position.z + 8f));
                enemy[i] = Instantiate(gameObjects[randomindex], randomposition, Quaternion.identity);
                enemy[i].transform.parent = gameObject.transform;
            }//create 3~6 enemy within airwall
        }//judge who enter the room and has entered the room or not
    }//enter the room
}
