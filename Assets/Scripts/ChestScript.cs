using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{

    public GameObject coin;
    public GameObject healFlask;
    public GameObject boxOpen;
    public GameObject boxClose;
    bool guncanhit = true;
    int playerDamage;
    float playerAttackRange;
    int currentHealth;
    [SerializeField] int maxHealth = 100;
    public BarScript healthBar;
    public LayerMask whatIsPlayer;
    bool dropCoin = false;
    [SerializeField] int dropCoinNumber = 10;
    private float lastTime; 
    private float curTime;

    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth();
        boxOpen.SetActive(false);
        //audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttackCheck();
        StatusCheck(); // check if the mushroom die
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            //audiosource.PlayOneShot(die);
        }
    }

    public void SetMaxHealth()
    {
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    // damage check functions
    bool IsSword()
    {
        playerDamage = GameObject.Find("maincharacter").GetComponent<WeaponChange>().swordDamage;
        playerAttackRange = GameObject.Find("maincharacter").GetComponent<WeaponChange>().swordAttackRange;
        return GameObject.Find("maincharacter").GetComponent<WeaponChange>().now_is_sword;
    }


    bool IsCloseToPlayer()
    {
        return Physics.CheckSphere(transform.position, playerAttackRange, whatIsPlayer);
    }
    void PlayerAttackCheck()
    {

        if (Input.GetMouseButtonDown(0) && IsSword() && IsCloseToPlayer())
        {
            TakeDamage(playerDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "laser" && guncanhit)
        {
            curTime = Time.time;
            TakeDamage(GameObject.Find("maincharacter").GetComponent<WeaponChange>().gunDamage);
            guncanhit = false;
            if (curTime - lastTime >= 0.5)   
            {
                guncanhit = true;
            }
        }
    }

    private void DropCoin()
    {
        //Transform c = Instantiate(coin.transform);
        for (int i = 0; i < dropCoinNumber; i++) Instantiate(coin.transform).localPosition = transform.position + new Vector3(i * 0.1f, 1, 0);
    }
    private void DropHealFlask()
    {
        Transform h = Instantiate(coin.transform);
        h.localPosition = transform.position += new Vector3(0, 1, 0);
    }
    void StatusCheck()
    {
        if (currentHealth <= 0 && !dropCoin)
        {
            dropCoin = true;
            Invoke(nameof(DropCoin), 1);
            Invoke(nameof(DropHealFlask), 1);
            Invoke(nameof(ShowOpenBox), 1);
            
        }
    }
    void ShowOpenBox()
    {
        Vector3 pos = boxClose.transform.position;
        boxClose.SetActive(false);
        Destroy(boxClose);
        boxOpen.SetActive(true);
        boxOpen.transform.position = pos;
        boxOpen.transform.position += new Vector3(0, -1, 0); 
    }
}

