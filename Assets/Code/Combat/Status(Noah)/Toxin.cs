using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Toxin", menuName = "Status/Toxin")]
public class ToxinSO : StatusSO
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
        while (localTimer > 0)
        {
           Debug.Log("I am at the start of while toxin, and timer = " + localTimer.ToString());
           target.GetComponent<EnemyBase>().Damage(_ticksPerSecond * _tickDamage);
           localTimer -= 1;
           yield return new WaitForSeconds(1);
        }
    }
}
