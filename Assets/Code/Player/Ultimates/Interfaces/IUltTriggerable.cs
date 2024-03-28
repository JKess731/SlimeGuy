using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUltTriggerable
{
    public float activationTime { get; set; }

    void HandleTriggers(GameObject enemy);

    IEnumerator OnActivate(float activationTime);
}
