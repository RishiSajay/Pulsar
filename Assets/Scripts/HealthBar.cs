using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider mySlider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(float health)
    {
        mySlider.maxValue = health;
        mySlider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        mySlider.value = health;
        fill.color = gradient.Evaluate(mySlider.normalizedValue);
    }

}