using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private AbilityBase primary;
    [SerializeField] private AbilityBase dash;
    [SerializeField] private AbilityBase secondary;

    private PlayerInput input;

    private void Start()
    {
        input = GetComponent<PlayerController>().playerInput;
    }
}
