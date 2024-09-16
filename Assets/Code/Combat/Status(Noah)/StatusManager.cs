using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public void StatusHandler(StatusSO status)
    {
        StartCoroutine(status.Apply(gameObject));
    }
}
