using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float knockback;

    protected Vector2 startPos;

    protected virtual void Start()
    {
        startPos = transform.position;
    }
}
