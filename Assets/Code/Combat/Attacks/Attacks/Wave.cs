using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : Attacks
{
    [Header("Wave Settings")]
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _child;

    [SerializeField] private float _lifeTime;


    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.GetComponent<EnemyBase>().Damage(base.damage);
        }
    }

    private void OnDestroy()
    {
        Instantiate(_child, _parent.transform.position, _parent.transform.rotation);
    }
}
