using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;

    [SerializeField] private TextMeshProUGUI _healthTxt;
    [SerializeField] private TextMeshProUGUI _primaryCooldownTxt;
    [SerializeField] private TextMeshProUGUI _secondaryCooldownTxt;
    [SerializeField] private TextMeshProUGUI _dashCooldownTxt;
    [SerializeField] private TextMeshProUGUI passiveCooldownTxt;

    [SerializeField] private Image _primaryAbilityImage;
    [SerializeField] private Image _secondaryAbilityImage;
    [SerializeField] private Image _dashAbilityImage;
    [SerializeField] private Image _passiveAbilityImage;

    [SerializeField] private Slider _primaryCooldown;
    [SerializeField] private Slider _secondaryCooldown;
    [SerializeField] private Slider _dashCooldown;
    [SerializeField] private Slider _passiveCooldown;

    [SerializeField] private Image[] _RelicImages;
    [SerializeField] private Image[] _RelicImageBackgrounds;

    private AbilityManager _abilityManager;

    public static UiManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        //Moved to awake to prevent null reference errors. Callbacks are not being called in the correct order.
        //Changed to gameobject.find instead of findobjectoftype
        _abilityManager = GameObject.Find("Ability Manager").GetComponent<AbilityManager>();
        _healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
        _healthTxt = GameObject.Find("Health Text").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            UpdatePrimaryAbilityImage(_abilityManager.Primary?.Icon);
            UpdateSecondaryAbilityImage(_abilityManager.Secondary?.Icon);
            UpdateDashAbilityImage(_abilityManager.Dash?.Icon);
        }
    }

    /// <summary>
    /// Use to update all icons on the UI, to resolve callback issues.
    /// </summary>
    public void UpdateAllIcons()
    {
            //Debug.Log("UI Get Primary: " + _abilityManager.Primary.Icon);
            //Debug.Log("UI Get Secondary: " + _abilityManager.Secondary.Icon);
        try
        {
            UpdatePrimaryAbilityImage(_abilityManager.Primary?.Icon);
            UpdateSecondaryAbilityImage(_abilityManager.Secondary?.Icon);
            UpdateDashAbilityImage(_abilityManager.Dash?.Icon);
        }
        catch (System.NullReferenceException)
        {
            Debug.LogWarning("One or more abilities are not assigned");
        }
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        float newHealth = math.clamp(health, 0, maxHealth);
        _healthBar.value = newHealth/ maxHealth;
        _healthTxt.SetText(newHealth + "/" + maxHealth);
    }

    public void UpdatePrimaryAbilityImage(Sprite Icon)
    {
        _primaryAbilityImage.sprite = Icon;
    }

    public void UpdateSecondaryAbilityImage(Sprite Icon)
    {
        _secondaryAbilityImage.sprite = Icon;
    }

    public void UpdateDashAbilityImage(Sprite Icon)
    {
        _dashAbilityImage.sprite = Icon;
    }

    public void UpdatePassiveAbilityImage(Sprite Icon)
    {
        _passiveAbilityImage.sprite = Icon;
    }

    public IEnumerator TextAndSliderAdjustment(AbilitySOBase attack, string type) //this coroutine is for the radial cooldown and text on abilites.
    {
        Slider modifiedSlider = null;
        TextMeshProUGUI modifiedText = null;
        float newValue = attack.CooldownTime;

        if (type == "P")
        {
            modifiedSlider = _primaryCooldown;
            modifiedText = _primaryCooldownTxt;
        }
        else if (type == "S")
        {
            modifiedSlider = _secondaryCooldown;
            modifiedText = _secondaryCooldownTxt;
        }
        else if (type == "D")
        {
            modifiedSlider = _dashCooldown;
            modifiedText = _dashCooldownTxt;
            Debug.Log("I used dash and got here " + attack.CooldownTime);
        }
        else if (type == "PA")
        {
            modifiedSlider = _passiveCooldown;
            modifiedText = passiveCooldownTxt;
        }

        yield return new WaitForSeconds(attack.ActivationTime);

        modifiedSlider.gameObject.SetActive(true);
        modifiedText.gameObject.SetActive(true);

        modifiedSlider.maxValue = attack.CooldownTime;
        modifiedSlider.value = modifiedSlider.maxValue;
        modifiedText.text = attack.CooldownTime.ToString();

        while (newValue > 0 && attack.CooldownTime > 1)
        {
            newValue -= 1;
            yield return new WaitForSeconds(1);
            modifiedSlider.value = newValue;
            modifiedText.text = newValue.ToString();
        }

        while (newValue > 0 && attack.CooldownTime < 1)
        {
            newValue -= 0.1f;
            yield return new WaitForSeconds(0.1f);
            modifiedSlider.value = newValue;
            if (newValue <= 0)
                if (newValue <= 0.1f)
                {
                    modifiedText.text = "0";
                }
                else
                {
                    modifiedText.text = newValue.ToString();
                }
        }

        while (newValue > 0 && attack.CooldownTime == 1)
        {
            newValue -= 0.2f;
            yield return new WaitForSeconds(0.2f);
            modifiedSlider.value = newValue;
            if (newValue < 0.2f)
            {
                modifiedText.text = "0";
            }
            else
            {
                modifiedText.text = newValue.ToString();
            }
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
            _RelicImages[index].sprite = relic.Icon;
            Color newColor = _RelicImageBackgrounds[index].color;
            newColor.a = 0.95f;
            _RelicImageBackgrounds[index].color = newColor;
            _RelicImages[index].gameObject.SetActive(true);
        }
        else
        {
            Color newColor = _RelicImageBackgrounds[index].color;
            newColor.a = 0.35f;
            _RelicImageBackgrounds[index].color = newColor;
            _RelicImages[index].gameObject.SetActive(false);
        }
    }
}
