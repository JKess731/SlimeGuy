using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SecondaryAttack : Attack
{
    protected void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(OnActivate(activationTime));
        }
    }

    public abstract IEnumerator OnActivate(float aTime);

    public virtual void OnDeactivate() { enabled = false; }
}
