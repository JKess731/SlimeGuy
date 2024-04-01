using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Vector3 targetPos;
    private float speed;
    private ASlimeGrenade parent;
    private bool started = false;

    private Vector3 lastPos;
    
    public void Init(Vector3 targetPos, float speed, ASlimeGrenade parent)
    {
        this.targetPos = targetPos;
        this.speed = speed;
        this.parent = parent;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (transform.position == targetPos)
        {
            parent.grenadeLanded = true;
            parent.landingPos = transform.position;
            Destroy(gameObject);
        }
        else
        {
            lastPos = transform.position;
            transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
        }
        
    }
}
