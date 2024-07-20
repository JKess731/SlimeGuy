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

    private void OnEnable()
    {
        try
        {
            input.GamePlay.Primary.started += context => primary.Activate(context);
            input.GamePlay.Dash.started += context => dash.Activate(context);
            input.GamePlay.Secondary.started += context => secondary.Activate(context);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }

    private void OnDisable()
    {
        try
        {
            input.GamePlay.Primary.started -= context => primary.Activate(context);
            input.GamePlay.Dash.started -= context => dash.Activate(context);
            input.GamePlay.Secondary.started -= context => secondary.Activate(context);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}
