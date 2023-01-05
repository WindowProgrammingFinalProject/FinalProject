using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 3.5f;
    public float jumpVelocity = 0f;
    bool canJump = false;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    public CharacterController characterController;
    public Transform cam;
    public bool IsPause = false;
    public BarScript healthBar;
    public BarScript shieldBar;
    public TMPro.TextMeshProUGUI textMeshProUGUI;


    public int currentHealth = 100;
    public int currentShield;
    public int maxShield = 20;
    public int maxHealth = 100;
    public int coinNumber = 0;
    private Animator myAnimator;

    private float lastTime;   
    private float curTime;
    private bool skill = false;
    private bool first = true;


    // test
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
        InitializePlayerStatus(); // initialize player's status after purchasing from store
        InitializeHealthStatus(); // initialize player's health
        InitializeShieldStatus(); // initialize player's shield

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // 鼠標消失
    }

    // Update is called once per frame
    void Update()
    {
        curTime = Time.time;
        myAnimator.SetBool("run", false);
        Movement();
        PauseAndResumeTheGame();
        PlayerAliveCheck();
        ShieldRecover();
        // test
        if (Input.GetKey(KeyCode.P))
        {
            GoToStoreScene();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && skill == false && (curTime - lastTime >= 20 || first ))
        {
            speed += 12;
            skill = true;
            lastTime = Time.time;
            first = false;
        }
        if(skill == true &&　curTime - lastTime >= 5)
        {
            skill = false;
            speed -= 12;
            lastTime = Time.time;
        }
    }

    private void Movement()
    {
        // 用角度進行方向控制
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (gameObject.GetComponent<WeaponChange>().now_is_sword)
        {
            if (direction.magnitude >= 0.1f)
            {
                myAnimator.SetBool("run", true);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f); // when using sword
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characterController.Move(moveDir.normalized * speed * Time.deltaTime);
                myAnimator.SetBool("run", true);
            }
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, cam.eulerAngles.y, transform.eulerAngles.z); // when using gun
            if (direction.magnitude >= 0.1f)
            {
                myAnimator.SetBool("run", true);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characterController.Move(moveDir.normalized * speed * Time.deltaTime);
                myAnimator.SetBool("run", true);
            }
        }

        // jump
        if (characterController.isGrounded)
        {
            canJump = false;
            jumpVelocity = 0f;
            if (Input.GetKey(KeyCode.Space))
            {
                jumpVelocity = jumpHeight;
                canJump = true;
            }
        }
        Vector3 yDirection = new Vector3(0f, jumpVelocity, 0f).normalized;
        if (yDirection.magnitude >= 0.5 && canJump) characterController.Move(yDirection * 5 * Time.deltaTime);
    }

    // pause the game using esc key
    private void PauseAndResumeTheGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPause = !IsPause;
            Cursor.visible = (IsPause) ? true : false;
            Cursor.lockState = (IsPause) ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = (IsPause) ? 0 : 1;
            // todo: show menu when pausing the game
        }
    }

    private void InitializeHealthStatus()
    {
        if (SceneManager.GetActiveScene().name == "Level1Scene")
        {
            currentHealth = maxHealth;
            coinNumber = 0;
        }
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);

    }
    public void TakeDamage(int damage)
    {
        if (currentShield <= 0)
        {
            currentHealth -= damage;
            currentShield = 0;
        }
        else currentShield -= damage;

        healthBar.GetComponent<BarScript>().SetHealth(currentHealth);
        shieldBar.GetComponent<BarScript>().SetHealth(currentShield);
    }

    public void PlayerAliveCheck()
    {

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("GameOverScene");
        }
    }

    // switch scene to store and carry informations such as health, speed...
    void GoToStoreScene()
    {
        Debug.Log("store");
        PlayerPrefs.SetInt("coin", coinNumber); // store the coin number, this will be used in other scene
        PlayerPrefs.SetInt("currentHealth", currentHealth); // same as above
        PlayerPrefs.SetInt("maxHealth", maxHealth);
        PlayerPrefs.SetInt("maxShield", maxShield);
        PlayerPrefs.SetFloat("speed", speed);
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("StoreScene");
    }
    public void addHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }
    private void InitializeShieldStatus()
    {
        if (SceneManager.GetActiveScene().name == "Level1Scene")
        {
            currentShield = maxShield;
        }
        shieldBar.SetMaxHealth(maxShield);
        currentShield = maxShield;
        shieldBar.SetHealth(currentShield);

    }

    // shield recover system
    public float period = 0.0f;
    int test = 0;
    private void ShieldRecover()
    {
        period += UnityEngine.Time.deltaTime;
        if (period > 2)
        {
            currentShield += 5;
            if (currentShield > maxShield) currentShield = maxShield;
            shieldBar.SetHealth(currentShield);
            period = 0;
        }
    }
    private void InitializePlayerStatus()
    {
        maxHealth = PlayerPrefs.GetInt("maxHealth");
        maxShield = PlayerPrefs.GetInt("maxShield");
        speed = PlayerPrefs.GetFloat("speed");
        coinNumber = PlayerPrefs.GetInt("coin");
        currentHealth = PlayerPrefs.GetInt("currentHealth");
        textMeshProUGUI = GameObject.Find("CoinNumber").GetComponent<TMPro.TextMeshProUGUI>();
        if (SceneManager.GetActiveScene().name == "Level1Scene")
        {
            coinNumber = 0;
            maxHealth = 100;
            maxShield = 20;
            speed = 5;
        }
        textMeshProUGUI.text = coinNumber.ToString();
    }
}