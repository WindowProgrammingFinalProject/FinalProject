using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMonStatusController : MonoBehaviour
{

    private int currentHealth;
    public BarScript healthBar;

    [SerializeField] int maxHealth = 100;
    bool alive = true;
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

        if (Input.GetMouseButtonDown(0) && IsSword() && IsCloseToPlayer())
        {
            TakeDamage(playerDamage);
            Debug.Log("take damage");
        }

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
            alive = false;
            transform.localPosition += new Vector3(0, 0.1f, 0);
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0)); // rotate the enemy's corpse (lying on the ground)
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
        // todo
        // check player layer sphere
        return Physics.CheckSphere(transform.position, playerAttackRange, whatIsPlayer);
        
    }
}
