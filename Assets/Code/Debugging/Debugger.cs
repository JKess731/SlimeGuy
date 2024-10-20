using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) && Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.F2) && Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = .3f;
        }

        if (Input.GetKeyDown(KeyCode.F3) && Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 0f;
        }
    }
}
