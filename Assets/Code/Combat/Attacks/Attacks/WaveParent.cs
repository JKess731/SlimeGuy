using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveParent : Attacks
{
    [Header("Wave Settings")]
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _child;

    private WaveStruct _waveStruct;

    protected void Start()
    {
        Destroy(gameObject, _waveStruct.ActivationTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.GetComponent<EnemyBase>().Damage(base._damage);
        }
    }

    private void OnDestroy()
    {
        Instantiate(_child, _parent.transform.position, _parent.transform.rotation);
    }

    public void SetWaveStruct(WaveStruct waveStruct)
    {
        _waveStruct = waveStruct;
    }
}
