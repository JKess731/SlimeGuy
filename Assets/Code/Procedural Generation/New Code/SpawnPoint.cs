using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private ProceduralGenerator genScript;
    public DoorTypes doorNeeded;

    public GameObject parentRoom;

    private void Awake()
    {
        genScript = FindAnyObjectByType<ProceduralGenerator>();
    }

    private void Start()
    {
        parentRoom = transform.parent.gameObject;
        genScript.SpawnPointEnqueue(this);
    }
}
