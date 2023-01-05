using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float generateX1;
    [SerializeField] float generateX2;
    [SerializeField] float generateZ1;
    [SerializeField] float generateZ2;
    [SerializeField] float generateY = 0.47f;
    public GameObject[] gameObjects;//kinds of enemy type, now only mushroom
    private bool everentertheroom = false;//has entered the room or not
    GameObject[] enemy = new GameObject[6];//create <= 6 enemy
    int enemyNumber;//enemy number
    [SerializeField]int minNumOfEnemy = 3;
    [SerializeField]int maxNumOfEnemy = 6;

    //when player enter the room, create 3~6 enemy
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "maincharacter" && !everentertheroom)
        {

            everentertheroom = true;//has entered thr room
            enemyNumber = Random.Range(minNumOfEnemy, maxNumOfEnemy + 1);//random enemy number

            for (int i = 0; i < enemyNumber; i++)
            {
                int randomindex = Random.Range(0, gameObjects.Length);
                Vector3 randomposition = new Vector3(Random.Range(transform.position.x + generateX1, transform.position.x + generateX2), generateY, Random.Range(transform.position.z + generateZ1, transform.position.z + generateZ2));
                enemy[i] = Instantiate(gameObjects[randomindex], randomposition, Quaternion.identity);
                //enemy[i].transform.parent = gameObject.transform; // 改用空氣牆座標來決定生成位置
            }//create 3~6 enemy within airwall
        }//judge who enter the room and has entered the room or not
    }//enter the room
}
