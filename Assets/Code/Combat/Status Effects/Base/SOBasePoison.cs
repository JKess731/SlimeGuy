using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "PoisonDamage", menuName = "Status Effects/Poison")]
public class SOBasePoison : Effect
{
    
    [SerializeField] float damage;

    public float GetDamage()
    {
        return damage;
    }

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
