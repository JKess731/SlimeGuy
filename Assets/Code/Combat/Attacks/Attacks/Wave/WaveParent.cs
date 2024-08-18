using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveParent : Attacks
{
    [SerializeField] private GameObject _child;
    private float _activationTime;

    protected void Start()
    {
        Destroy(gameObject, _activationTime);
    }

    private void OnDestroy()
    {
        if (_child != null)
        {
            Instantiate(_child, transform.position, transform.rotation);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected");
        // If the object is an enemy
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("Enemy hit");

            EnemyBase enemy = collision.GetComponent<EnemyBase>();

            //Else, add the enemy to the hashset and damage it
            enemy.Damage(base._damage);

        }
    }

    public void SetParentWave(WaveStruct parent, WaveStruct child)
    {
        _damage = parent.Damage;
        _knockback = parent.Knockback;
        _activationTime = parent.ActivationTime;

        try
        {
            _child.GetComponent<WaveChild>().SetWaveStruct(child);
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
        }
    }   
}
