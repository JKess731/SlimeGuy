using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryAttack : Attack
{
    public virtual IEnumerator OnActivate(float aTime) { yield return null; }
    public virtual void OnDeactivate() { enabled = false; }
}
