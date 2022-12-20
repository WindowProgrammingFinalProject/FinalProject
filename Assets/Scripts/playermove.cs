using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    public GameObject player;
    private int speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }
    private void movement()//方法
    {   //採用直接改變物件座標的方式
        //一、向右走
        if (Input.GetKey("d"))//輸入.來自鍵盤(“d”)
        {
            this.gameObject.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
        }  //此類別.這個物件.座標系統.位移(delta向量)

        //二、向左走；依照一、的作法會發現物件飆很快，因此要乘上Time.deltaTime來延遲。
        if (Input.GetKey("a"))
        {
            this.gameObject.transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * speed);
        }
        //向上走 //可以直接使用Vector的屬性Vector2.up，就不需要new一個變數
        if (Input.GetKey("w"))
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        }
        //向下走
        if (Input.GetKey("s"))
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * speed);
        }

    }
}
