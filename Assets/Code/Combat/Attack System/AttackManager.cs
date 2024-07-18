using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private Ability primary;
    [SerializeField] private Ability dash;
    [SerializeField] private Ability secondary;
    [SerializeField] private Ability secondary2;

    private PlayerInput playerInput;
}
