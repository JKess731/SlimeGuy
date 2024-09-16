using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    [SerializeField] private SingleRoomController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            controller.StartNextWave();
            gameObject.SetActive(false);
        }
    }
}
