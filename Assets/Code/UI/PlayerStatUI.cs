using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthTXT;
    [SerializeField] private TextMeshProUGUI attackTXT;
    [SerializeField] private TextMeshProUGUI defenseTXT;
    [SerializeField] private TextMeshProUGUI speedTxt;

    [SerializeField] PlayerScript player;
    private void Start()
    {
        healthTXT.SetText("health: " + player.returnStats(Stats.statsName.MAXHEALTH));
        attackTXT.SetText("attack: " + player.returnStats(Stats.statsName.ATTACK));
        defenseTXT.SetText("defense: " + player.returnStats(Stats.statsName.DEFENSE));
        speedTxt.SetText("speed: " + player.returnStats(Stats.statsName.SPEED));
    }

    /// <summary>
    /// Sets the text based on the stat boost type
    /// </summary>
    /// <param name="boostType"></param>
    public void setText(Stats.statBoost boostType)
    {
        switch (boostType)
        {
            case Stats.statBoost.healthUP:
                healthTXT.SetText("health: " + player.returnStats(Stats.statsName.MAXHEALTH));
                break;
            case Stats.statBoost.attackUP:
                attackTXT.SetText("attack: " + player.returnStats(Stats.statsName.ATTACK));
                break;
            case Stats.statBoost.defenseUP:
                defenseTXT.SetText("defense: " + player.returnStats(Stats.statsName.DEFENSE));
                break;
            case Stats.statBoost.speedUP:
                speedTxt.SetText("speed: " + player.returnStats(Stats.statsName.SPEED));
                break;
        }
    }
}
