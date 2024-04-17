using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Status Effects", fileName = "Damage Over Time")]
public class SOBaseDamageOverTime : Effect
{
    public float time;
    public float damagePerDeal;
}
