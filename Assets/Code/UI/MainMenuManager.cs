using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGameOnClick()
    {
        SceneManager.LoadScene("ProceduralGen");
    }

    public void QuitGameOnClick()
    {
        Application.Quit();
    }
}
