using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Edison Li
public class ability : ScriptableObject
{
    public string abilityName;
    public int damage;

    //Duration Control
    public int activeTime;
    public int activeCoolDown;

    //virtual - Unity version of overriding, abstract
    public virtual void Activate(GameObject parent){}
    public virtual void Activate() { }
    public virtual void Attack() {}
}
