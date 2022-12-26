using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public int damage;


    void Start()
    {
        Invoke(nameof(Destroy), 3);
    }

    private void Update()
    {
        
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("maincharacter").GetComponent<PlayerMovement>().TakeDamage(damage);
            Destroy(gameObject);
            Debug.Log("hit player!!");
        }
    }
}
