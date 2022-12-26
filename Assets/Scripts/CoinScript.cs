using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    public AudioSource audioSource; // pickup coin sound effect
    public AudioClip audioClip;
    public TMPro.TextMeshProUGUI textMeshProUGUI;
    [SerializeField] int score = 1;
    bool canPick = true;
    string point;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        textMeshProUGUI = GameObject.Find("CoinNumber").GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        transform.Rotate(90 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && canPick)
        {
            Debug.Log("hitt");
            canPick = false;
            audioSource.PlayOneShot(audioClip);
            other.GetComponent<PlayerMovement>().coinNumber += score;
            point = (other.GetComponent<PlayerMovement>().coinNumber).ToString();
            GetComponent<MeshRenderer>().enabled = false;
            Invoke(nameof(DestroyCoinAndAddScore), 0.4f);
        }
    }
    void DestroyCoinAndAddScore()
    {
        textMeshProUGUI.text = point;
        Destroy(gameObject);
    }
}
