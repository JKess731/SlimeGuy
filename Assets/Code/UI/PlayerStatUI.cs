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

    [SerializeField] player player;
    private void Start()
    {
        healthTXT.SetText("health: " + player.returnStats(stats.statsName.MAXHEALTH));
        attackTXT.SetText("attack: " + player.returnStats(stats.statsName.ATTACK));
        defenseTXT.SetText("defense: " + player.returnStats(stats.statsName.DEFENSE));
        speedTxt.SetText("speed: " + player.returnStats(stats.statsName.SPEED));
    }

    public void setText(stats.statBoost boostType)
    {
        switch (boostType)
        {
            case stats.statBoost.healthUP:
                healthTXT.SetText("health: " + player.returnStats(stats.statsName.MAXHEALTH));
                break;
            case stats.statBoost.attackUP:
                attackTXT.SetText("attack: " + player.returnStats(stats.statsName.ATTACK));
                break;
            case stats.statBoost.defenseUP:
                defenseTXT.SetText("defense: " + player.returnStats(stats.statsName.DEFENSE));
                break;
            case stats.statBoost.speedUP:
                speedTxt.SetText("speed: " + player.returnStats(stats.statsName.SPEED));
                break;
        }
    }
}
