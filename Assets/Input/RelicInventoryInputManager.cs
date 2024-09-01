using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RelicInventoryInputManager : MonoBehaviour
{
    public static RelicInventoryInputManager instance;
    [SerializeField] private GameObject RelicInventory;
    private bool isOpen;
    public bool menuOpenCloseInput {  get; private set; }
    public InputActionAsset inputActionAsset;
    private InputAction moveAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        var actionMap = inputActionAsset.FindActionMap("Menu");
        moveAction = actionMap.FindAction("OpenMenu");
    }

    private void Start()
    {
        isOpen = false;
    }

    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    private void Update()
    {
        menuOpenCloseInput = moveAction.WasPressedThisFrame();
        if(menuOpenCloseInput)
        {
            Debug.Log("Input is working correctly");
            if (!isOpen)
            {
                isOpen = true;
                RelicInventory.SetActive(true);
                Debug.Log("Opening Menu");
            }
            else
            {
                isOpen = false;
                RelicInventory.SetActive(false);
                Debug.Log("Closing Menu");
            }
        }
    }
}
