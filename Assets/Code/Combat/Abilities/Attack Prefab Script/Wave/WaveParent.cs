using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveParent : Attacks
{
    [SerializeField] private GameObject _child;
    [SerializeField] private Transform _spawnOffset;
    private float _activationTime;

    private WaveStruct _childStruct;

    protected void Start()
    {
        StartCoroutine(DestroyWave());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the object is an enemy
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("Enemy hit");

            EnemyBase enemy = collision.GetComponent<EnemyBase>();

            //Else, add the enemy to the hashset and damage it
            enemy.Damage(_damage);

        }
    }

    public void SetParentWave(WaveStruct parent, WaveStruct child)
    {
        _damage = parent.Damage;
        _knockback = parent.Knockback;
        _activationTime = parent.ActivationTime;
        _childStruct = child;

        try
        {
            _child.GetComponent<WaveChild>().SetWaveStruct(child);
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
        }
    }
    
    private IEnumerator DestroyWave()
    {
        yield return new WaitForSeconds(_activationTime);

        if (_child != null)
        {
            GameObject waveChild = Instantiate(_child, _spawnOffset.position, transform.rotation);
            waveChild.GetComponent<WaveChild>().SetWaveStruct(_childStruct);
        }

        Destroy(gameObject);
    }
}
