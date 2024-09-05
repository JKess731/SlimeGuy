using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider absorptionBar;

    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private TextMeshProUGUI absorptionTxt;
    [SerializeField] private TextMeshProUGUI primaryCooldownTxt;
    [SerializeField] private TextMeshProUGUI secondaryCooldownTxt;
    [SerializeField] private TextMeshProUGUI dashCooldownTxt;
    [SerializeField] private TextMeshProUGUI passiveCooldownTxt;

    [SerializeField] private Image primaryAbilityImage;
    [SerializeField] private Image secondaryAbilityImage;
    [SerializeField] private Image dashAbilityImage;
    [SerializeField] private Image passiveAbilityImage;

    [SerializeField] private Slider primaryCooldown;
    [SerializeField] private Slider secondaryCooldown;
    [SerializeField] private Slider dashCooldown;
    [SerializeField] private Slider passiveCooldown;

    [SerializeField] private Image[] RelicImages;
    [SerializeField] private Image[] RelicImageBackgrounds;

    private AbilityManager _abilityManager;

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
        _abilityManager = FindObjectOfType<AbilityManager>();
        healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
        absorptionBar = GameObject.Find("Absorption Bar").GetComponent<Slider>();
        healthTxt = GameObject.Find("Health Text").GetComponent<TextMeshProUGUI>();
        absorptionTxt = GameObject.Find("Absorption Text").GetComponent<TextMeshProUGUI>();

        try
        {
            UpdatePrimaryAbilityImage(_abilityManager.Primary.Icon);
            UpdateSecondaryAbilityImage(_abilityManager.Secondary.Icon);
            UpdateDashAbilityImage(_abilityManager.Dash.Icon);
        }
        catch (System.NullReferenceException)
        {
            Debug.LogWarning("One or more abilities are not assigned");
        }
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

    public void UpdatePrimaryAbilityImage(Sprite Icon)
    {
        primaryAbilityImage.sprite = Icon;
    }

    public void UpdateSecondaryAbilityImage(Sprite Icon)
    {
        secondaryAbilityImage.sprite = Icon;
    }

    public void UpdateDashAbilityImage(Sprite Icon)
    {
        dashAbilityImage.sprite = Icon;
    }

    public void UpdatePassiveAbilityImage(Sprite Icon)
    {
        passiveAbilityImage.sprite = Icon;
    }

    public IEnumerator TextAndSliderAdjustment(AbilityBase attack, string type) //this coroutine is for the radial cooldown and text on abilites.
    {
        Debug.Log("I got to TextAndSliderAdjustment");
        Slider modifiedSlider = null;
        TextMeshProUGUI modifiedText = null;
        float newValue = attack.Behavior.CooldownTime;
        if(type == "P")
        {
            modifiedSlider = primaryCooldown;
            modifiedText = primaryCooldownTxt;
        }
        else if(type == "S")
        {
            modifiedSlider = secondaryCooldown;
            modifiedText = secondaryCooldownTxt;
        }
        else if(type == "D")
        {
            modifiedSlider = dashCooldown;
            modifiedText = dashCooldownTxt;
        }
        else if (type == "PA")
        {
            modifiedSlider = passiveCooldown;
            modifiedText = passiveCooldownTxt;
        }
        yield return new WaitForSeconds(attack.Behavior.ActivationTime);

        modifiedSlider.gameObject.SetActive(true);
        modifiedText.gameObject.SetActive(true);

        modifiedSlider.maxValue = attack.Behavior.CooldownTime;
        modifiedSlider.value = modifiedSlider.maxValue;
        modifiedText.text = attack.Behavior.CooldownTime.ToString(); 

        while(newValue > 0)
        {
            newValue -= 1;
            yield return new WaitForSeconds(1);
            modifiedSlider.value = newValue;
            modifiedText.text = newValue.ToString();
        }

        modifiedSlider.gameObject.SetActive(false);
        modifiedText.gameObject.SetActive(false);
        modifiedSlider.maxValue = 1;
        modifiedText.text = "";
    }

    public void UpdateRelicImage(RelicSO relic, int index, bool add)
    {
        if (add)
        {
            RelicImages[index].sprite = relic.spriteIcon;
            Color newColor = RelicImageBackgrounds[index].color;
            newColor.a = 0.95f;
            RelicImageBackgrounds[index].color = newColor;
            RelicImages[index].gameObject.SetActive(true);
        }
        else
        {
            Color newColor = RelicImageBackgrounds[index].color;
            newColor.a = 0.35f;
            RelicImageBackgrounds[index].color = newColor;
            RelicImages[index].gameObject.SetActive(false);
        }
    }
}
