using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodRoom : MonoBehaviour
{
    [SerializeField] Transform flask;
    [SerializeField] public AudioSource audioSource;
    private bool hasEntered = false;
    GameObject statue;

    // Start is called before the first frame update
    void Start()
    {
        statue = GameObject.Find("angelStatue");
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "maincharacter" && !hasEntered)
        {
            hasEntered = true;
            other.GetComponent<PlayerController>().addHealth();
            audioSource.Play();
            Invoke(nameof(HasEnteredTheRoom), 0.3f);
        }
    }
    void HasEnteredTheRoom()
    {
        statue.transform.Rotate(new Vector3(-71.8f, 0, 0) + statue.transform.position);
        Invoke(nameof(DestroyObject), 2f);
    }
    void DestroyObject()
    {
        Destroy(statue);
        
    }
}
