using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MushroomMonStatusController : MonoBehaviour
{

    private int currentHealth;
    public BarScript healthBar;

    [SerializeField] int maxHealth = 45;
    bool now_is_sword;
    int playerDamage;
    float playerAttackRange;
    public LayerMask whatIsPlayer;

    void Start()
    {
        SetMaxHealth();
    }

    void Update()
    {
        PlayerAttackCheck();
        StatusCheck(); // check if the mushroom die
    }

    // call the function below to change mushroom's health status
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void SetMaxHealth()
    {
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    void StatusCheck()
    {
        if (currentHealth <= 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0)); // rotate the enemy's corpse (lying on the ground)
            gameObject.GetComponent<CapsuleCollider>().enabled = false; // disable collider
            gameObject.GetComponent<NavMeshAgent>().enabled = false; // disable navMeshAgent

            Invoke(nameof(DestroyMushroom), 3);
        }
    }

    void DestroyMushroom()
    {
        Destroy(gameObject);
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
            //GameObject.Find("maincharacter").transform.LookAt(transform); // 玩家轉向敵人，但感覺有點生硬
            TakeDamage(playerDamage);
        }
    }
}
