using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Edison Li
//https://www.youtube.com/watch?v=ZHOWqF-b51k&t=1219s

[CreateAssetMenu(fileName = "InputReader", menuName = "InputReader")]
public class InputReader : ScriptableObject, PlayerInput.IGamePlayActions
{
    private PlayerInput playerInput;

    public void SetGamePlay()
    {
        playerInput.GamePlay.Enable();
    }

    public void OnEnable()
    {
        if (playerInput == null)
        {
            Debug.Log("Player Input is created");
            playerInput = new PlayerInput();
            playerInput.GamePlay.SetCallbacks(this);
            Debug.Log("SetCallback");
            SetGamePlay();
        }
    }

    public event Action<Vector2> movementEvent;
    public event Action movementEventCancel;

    public event Action dashEvent;
    public event Action dashEventCancel;


    public void OnAbsorb(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            dashEvent?.Invoke();
        }

        if(context.phase == InputActionPhase.Canceled)
        {
            dashEventCancel?.Invoke();
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            movementEvent?.Invoke(context.ReadValue<Vector2>());
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            movementEventCancel?.Invoke();
        }
    }

    public void OnShotgun(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSpawnMinion(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
