using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisionDetection : MonoBehaviour
{
    private Attack controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = transform.parent.GetComponent<Attack>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "enemy")
        {
            controller.enemiesToAttack.Add(collision.gameObject);
        }
    }
}
