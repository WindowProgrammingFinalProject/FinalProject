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
    private void movement()//��k
    {   //�ĥΪ������ܪ���y�Ъ��覡
        //�@�B�V�k��
        if (Input.GetKey("d"))//��J.�Ӧ���L(��d��)
        {
            this.gameObject.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
        }  //�����O.�o�Ӫ���.�y�Шt��.�첾(delta�V�q)

        //�G�B�V�����F�̷Ӥ@�B���@�k�|�o�{�����t�ܧ֡A�]���n���WTime.deltaTime�ө���C
        if (Input.GetKey("a"))
        {
            this.gameObject.transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * speed);
        }
        //�V�W�� //�i�H�����ϥ�Vector���ݩ�Vector2.up�A�N���ݭnnew�@���ܼ�
        if (Input.GetKey("w"))
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        }
        //�V�U��
        if (Input.GetKey("s"))
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * speed);
        }

    }
}
