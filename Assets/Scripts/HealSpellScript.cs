using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpellScript : MonoBehaviour
{

    [SerializeField] Transform flask;
    [SerializeField] public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(30, 0, 0));
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "maincharacter")
        {            
            other.GetComponent<PlayerMovement>().addHealth();
            audioSource.Play();
            Invoke(nameof(DestroyObject), 0.3f);
        }
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
