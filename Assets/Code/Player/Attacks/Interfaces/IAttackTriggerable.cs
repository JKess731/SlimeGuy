using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackTriggerable
{
    void HandleTriggers(GameObject enemy);
    void HandleInput();

    IEnumerator OnActivate(float activationTime);

}
