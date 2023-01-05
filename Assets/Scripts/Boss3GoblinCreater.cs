using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3GoblinCreater : MonoBehaviour
{
    //when time = 8(s), create 3 Goblin
    //then evety 20(s) later, create 4 Goblin  (28s, 48s, 68s...)
    [SerializeField] float generateX1;
    [SerializeField] float generateX2;
    [SerializeField] float generateZ1;
    [SerializeField] float generateZ2;
    public GameObject[] gameObjects;//kinds of enemy type, now only Goblin
    private bool coolingDownTime = false;
    GameObject[] enemy = new GameObject[15];//create <= 15 enemy
    int enemyNumber = 0;//enemy number
    int createNumber;
    float startTime;//when first goblin was created, record the time
    float curTime;//Update time, when time = 8, 28, 48, 68, create new goblin
    float round = 0;//Goblin has been created how many rounds
    private void Start()
    {
        startTime = Time.time;
        Debug.Log(startTime);
    }

    private void Update()
    {
        curTime = Time.time;
        UpdateCoolingTime();
    }

    private void UpdateCoolingTime()
    {
        if ((int)(curTime - startTime) == (int)(8 + round * 20))
        {
            coolingDownTime = true;
            CallNewGoblin();
        }
    }

    private void CallNewGoblin()
    {
        if (coolingDownTime)
        {
            coolingDownTime = false;
            if (round == 0) createNumber = 3;
            else createNumber = 4;
            round++;
            for (int i = enemyNumber, j = enemyNumber; i < createNumber + j; i++, enemyNumber++)
            {
                Vector3 randomposition = new Vector3(Random.Range(transform.position.x + generateX1, transform.position.x + generateX2), 0.47f, Random.Range(transform.position.z + generateZ1, transform.position.z + generateZ2));
                enemy[i] = Instantiate(gameObjects[0], randomposition, Quaternion.identity);
                Debug.Log("create Goblin");
                //if(i == 0)
                //{
                //    startTime = Time.time;
                //}
            }
        }
    }
}