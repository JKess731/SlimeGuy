using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
    }
}
