using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bleed", menuName = "Status/Bleed")]
public class Bleed : StatusSO
{
    [SerializeField] private float _tickDamage;
    public float tickDamage { get => _tickDamage; protected set => _tickDamage = value; }
    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }
    public override IEnumerator Apply(GameObject target)
    {
        float localTimer = _Timer;
        float tickDamage = _tickDamage;
        while (localTimer > 0)
        {
            Debug.Log("I am at the start of while bleed, and timer = " + localTimer.ToString());
            target.GetComponent<EnemyBase>().Damage(_ticksPerSecond * tickDamage);
            localTimer -= 1;
            tickDamage += 2;
            yield return new WaitForSeconds(1);
        }
    }
}
