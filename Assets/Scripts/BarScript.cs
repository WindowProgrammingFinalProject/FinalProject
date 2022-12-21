using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue); // normalized 可以把值變成從 0f - 1f
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    SetHealth(50);
        //}
    }
}
