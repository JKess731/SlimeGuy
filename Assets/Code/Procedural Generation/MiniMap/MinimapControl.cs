using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapControl : MonoBehaviour
{
    [SerializeField] private GameObject fullMap;
    [SerializeField] private GameObject cornerMap;

    private void Start()
    {
        cornerMap.SetActive(true);
        fullMap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchMaps();
    }

    private void SwitchMaps()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (fullMap.active == true)
            {
                cornerMap.SetActive(true);
                fullMap.SetActive(false);
            }
            else
            {
                fullMap.SetActive(true);
                cornerMap.SetActive(false);
            }
        }
    }
}
