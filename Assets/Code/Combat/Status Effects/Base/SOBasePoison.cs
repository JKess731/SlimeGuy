using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoisonDamage", menuName = "Status Effects/Poison")]
public class SOBasePoison : Effect
{
    public float damage;
    public int intervals;
    public float totalTime;

    public override void ApplyModifier(GameObject target)
    {
        EnemyBase enemyBase = target.GetComponent<EnemyBase>();
        enemyBase.Damage(damage/intervals); 
    }

    public IEnumerator DamageOverTime(float t, GameObject target)
    {
        float currentTime = 0;
        while (currentTime < t)
        {
            currentTime += Time.deltaTime;
            yield return new WaitForSeconds(t/intervals);
            ApplyModifier(target);
        }
    }
}
