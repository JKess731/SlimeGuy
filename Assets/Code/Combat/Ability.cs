using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public class Ability : ScriptableObject
{
    [SerializeField] private string abilityName;
    public void Activate(InputAction.CallbackContext context)
    {
        Debug.Log(abilityName + ": Activated");
    }
}
