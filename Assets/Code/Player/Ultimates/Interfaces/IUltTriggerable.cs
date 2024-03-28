using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUltTriggerable
{
    void HandleTriggers(GameObject enemy);
    void HandleInput();

    IEnumerator OnActivate(float activationTime);
}
