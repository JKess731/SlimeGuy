using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour
{
    [SerializeField] private string relicName;
    [SerializeField] private string relicType;
    [SerializeField] private string relicDescription;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player")) {Pickup();}
    }

    /// <summary>
    /// Called when the player picks up the relic meant to be overridden
    /// </summary>
    public virtual void Pickup(){}
}
