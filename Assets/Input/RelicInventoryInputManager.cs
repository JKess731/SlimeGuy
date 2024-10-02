using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RelicInventoryInputManager : MonoBehaviour
{
    public static RelicInventoryInputManager instance;
    [SerializeField] private GameObject relicInventory;
    [SerializeField] private GameObject relicInventoryBG;
    [SerializeField] private GameObject relicChest;
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
        relicInventoryBG.SetActive(false);
        relicInventory.SetActive(false);
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
                relicInventory.SetActive(true);
                relicInventoryBG.SetActive(true);
                Debug.Log("Opening Menu");
            }
            else
            {
                isOpen = false;
                relicInventory.SetActive(false);
                relicInventoryBG.SetActive(false);
                Debug.Log("Closing Menu");
            }
        }

        if(Input.GetKeyDown(KeyCode.V) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 playerPos = GameObject.Find("Player").transform.position;
            playerPos.x += 5;
            Instantiate(relicChest, playerPos, Quaternion.identity);
        }
    }
}
