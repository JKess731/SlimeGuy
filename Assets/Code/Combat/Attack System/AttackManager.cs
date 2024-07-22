using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private Ability primary;
    [SerializeField] private Ability dash;
    [SerializeField] private Ability secondary;
    [SerializeField] private Ability secondary2;

    private PlayerInput input;

    private void Start()
    {
        input = GetComponent<PlayerController>().playerInput;
    }
}
