using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public int damage;


    void Start()
    {
        Invoke(nameof(DestroyObject), 3);
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           other.GetComponent<PlayerController>().TakeDamage(damage);
           Destroy(gameObject);
        }
    }
}
