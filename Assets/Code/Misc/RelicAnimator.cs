using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicAnimator : MonoBehaviour
{
    [SerializeField] float moveDistance;
    [SerializeField] float moveSpeed;
    private Vector3 startPos;
    private Vector3 direction;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (transform.position.y >= startPos.y + moveDistance)
        {
            direction = Vector3.down;
        }
        else if (transform.position.y <= startPos.y)
        {
            direction = Vector3.up;
        }
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
