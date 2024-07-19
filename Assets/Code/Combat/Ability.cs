using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public void Activate(string name)
    {
        Debug.Log(name + ": Activated");
    }
}
