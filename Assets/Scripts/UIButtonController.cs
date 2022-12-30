using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonController : MonoBehaviour
{
    //UI
    public GameObject maxHealthPriceText;
    public GameObject maxShieldPriceText;
    public GameObject maxSpeedPriceText;
    public GameObject coinText;

    // price
    public int maxHealthPrice = 15;
    public int maxShieldPrice = 15;
    public int maxSpeedPrice = 15;
    public int HealUpPrice = 15;

    public int currentCoin;// = PlayerPrefs.GetInt("coin");
    public int currentHealth;// = PlayerPrefs.GetInt("currentHealth");
    public int maxHealth;// = PlayerPrefs.GetInt("maxHealth");
    public int maxShield;// = PlayerPrefs.GetInt("maxShield");
    public float speed;// = PlayerPrefs.GetFloat("speed");

    private void Start()
    {
        currentCoin = PlayerPrefs.GetInt("coin");
        currentHealth = PlayerPrefs.GetInt("currentHealth");
        maxHealth = PlayerPrefs.GetInt("maxHealth");
        maxShield = PlayerPrefs.GetInt("maxShield");
        speed = PlayerPrefs.GetFloat("speed");
        //UpdatePrice();
    }

    private void Update()
    {
        if (currentHealth > 0) UpdatePrice();

    }

    
    public void MaxHealthButton()
    {
        UpdatePrice();
        if (currentCoin - maxHealthPrice >= 0)
        {
            currentCoin -= maxHealthPrice;
            if (maxHealthPrice == 15)
            {
                maxHealth = 120;
            }
            else if (maxHealthPrice == 20)
            {
                maxHealth = 150;
            }
        }
    }
    public void MaxShieldButton()
    {
        UpdatePrice();
        if (currentCoin - maxShieldPrice >= 0)
        {
            currentCoin -= maxShieldPrice;
            if (maxShieldPrice == 15) maxShield = 30;
            else if (maxShieldPrice == 20) maxShield = 40;
        }
    }
    public void MaxSpeedButton()
    {
        UpdatePrice();
        if (currentCoin - maxSpeedPrice >= 0)
        {
            currentCoin -= maxSpeedPrice;
            if (maxSpeedPrice == 15) speed = 7;
            else if (maxSpeedPrice == 20) speed = 9;
        }
    }
    public void HealUpButton()
    {
        UpdatePrice();
        if (currentCoin - HealUpPrice >= 0)
        {
            currentCoin -= HealUpPrice;
            currentHealth = maxHealth;
        }
    }
    public void GoToNextLevelButton()
    {
        PlayerPrefs.SetInt("coin", currentCoin); // store the coin number, this will be used in other scene
        PlayerPrefs.SetInt("currentHealth", currentHealth); // same as above
        PlayerPrefs.SetInt("maxHealth", maxHealth);
        PlayerPrefs.SetInt("maxShield", maxShield);
        PlayerPrefs.SetFloat("speed", speed);
        if (PlayerPrefs.GetString("CurrentLevel") == "Level1Scene") SceneManager.LoadScene("Level2Scene");
        else if (PlayerPrefs.GetString("CurrentLevel") == "Level2Scene") SceneManager.LoadScene("Level3Scene");
    }
    void UpdatePrice()
    {
        if (maxHealth == 100) maxHealthPrice = 15;
        else if (maxHealth == 120) maxHealthPrice = 20;
        else if (maxHealth == 150) maxHealthPrice = 99999;

        if (maxShield == 20) maxShieldPrice = 15;
        else if (maxShield == 30) maxShieldPrice = 20;
        else if (maxShield == 40) maxShieldPrice = 99999;

        if (speed == 5) maxSpeedPrice = 15;
        else if (speed == 7) maxSpeedPrice = 20;
        else if (speed == 9) maxSpeedPrice = 99999;

        // update ui
        maxHealthPriceText.GetComponent<TMPro.TextMeshProUGUI>().text = maxHealthPrice.ToString();
        maxShieldPriceText.GetComponent<TMPro.TextMeshProUGUI>().text = maxShieldPrice.ToString();
        maxSpeedPriceText.GetComponent<TMPro.TextMeshProUGUI>().text = maxSpeedPrice.ToString();
        coinText.GetComponent<TMPro.TextMeshProUGUI>().text = currentCoin.ToString();
    }
}
