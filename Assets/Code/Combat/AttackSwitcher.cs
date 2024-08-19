using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject slimeWaveControl;
    [SerializeField] private GameObject slimeWhipControl;
    [SerializeField] private GameObject slimePushControl;

    private int current = 1;

    private void Start()
    {
        slimeWaveControl.SetActive(false);
        slimeWhipControl.SetActive(false);
        slimePushControl.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (current != 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                slimeWaveControl.SetActive(false);
                slimeWhipControl.SetActive(true);
                slimePushControl.SetActive(false);
                current = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                slimeWaveControl.SetActive(false);
                slimeWhipControl.SetActive(false);
                slimePushControl.SetActive(true);
                current = 3;
            }
        }

        if (current != 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                slimeWaveControl.SetActive(true);
                slimeWhipControl.SetActive(false);
                slimePushControl.SetActive(false);
                current = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                slimeWaveControl.SetActive(false);
                slimeWhipControl.SetActive(false);
                slimePushControl.SetActive(true);
                current = 3;
            }
        }

        if (current != 3)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                slimeWaveControl.SetActive(true);
                slimeWhipControl.SetActive(false);
                slimePushControl.SetActive(false);
                current = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                slimeWaveControl.SetActive(false);
                slimeWhipControl.SetActive(true);
                slimePushControl.SetActive(false);
                current = 2;
            }
        }
    }
}
