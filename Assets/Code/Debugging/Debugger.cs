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

        //Audio Changer
        if (Input.GetKeyDown(KeyCode.F8) && Input.GetKey(KeyCode.LeftShift))
        {
            AudioManager.instance.IValleyTheme.setParameterByName("dangerLevel", 0);

            float value1 = 1234567f;
            AudioManager.instance.IValleyTheme.getParameterByName("dangerLevel", out value1);
            Debug.Log(value1);

            float value2 = 1234567f;
            AudioManager.instance.IValleyTheme.getParameterByName("enemyNear", out value2);
            Debug.Log(value2);
        }
        if (Input.GetKeyDown(KeyCode.F9) && Input.GetKey(KeyCode.LeftShift))
        {
            AudioManager.instance.IValleyTheme.setParameterByName("dangerLevel", 1);
            AudioManager.instance.IValleyTheme.setParameterByName("enemyNear", 1f, false);
            //StartCoroutine("quickDelay");

            float value1 = 1234567f;
            AudioManager.instance.IValleyTheme.getParameterByName("dangerLevel", out value1);
            Debug.Log(value1);

            float value2 = 1234567f;
            AudioManager.instance.IValleyTheme.getParameterByName("enemyNear", out value2);
            Debug.Log(value2);
        }
        if (Input.GetKeyDown(KeyCode.F10) && Input.GetKey(KeyCode.LeftShift))
        {
            AudioManager.instance.IValleyTheme.setParameterByName("dangerLevel", 2);

            float value1 = 1234567f;
            AudioManager.instance.IValleyTheme.getParameterByName("dangerLevel", out value1);
            Debug.Log(value1);

            float value2 = 1234567f;
            AudioManager.instance.IValleyTheme.getParameterByName("enemyNear", out value2);
            Debug.Log(value2);
        }
    }

    private IEnumerator quickDelay()
    {
        yield return new WaitForSeconds(10f);
        AudioManager.instance.IValleyTheme.setParameterByName("dangerLevel", 0f, false);
        AudioManager.instance.IValleyTheme.setParameterByName("enemyNear", 0f, false);
    }
}
