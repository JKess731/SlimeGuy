using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider absorptionBar;

    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private TextMeshProUGUI absorptionTxt;

    public static UiManager instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("UIManager instance already exists. Destroying duplicate.");
            Destroy(gameObject);
        }

        healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
        absorptionBar = GameObject.Find("Absorption Bar").GetComponent<Slider>();
        healthTxt = GameObject.Find("Health Text").GetComponent<TextMeshProUGUI>();
        absorptionTxt = GameObject.Find("Absorption Text").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthBar.value = health / maxHealth;
        healthTxt.SetText(health + "/" + maxHealth);
    }

    public void UpdateAbsorptionBar(float absorption, float maxAbsorption)
    {
        absorptionBar.value = absorption / maxAbsorption;
        absorptionTxt.SetText(absorption + "/" + maxAbsorption);
    }
}
