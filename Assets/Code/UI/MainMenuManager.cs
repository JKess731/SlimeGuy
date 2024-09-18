using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGameOnClick()
    {
        SceneManager.LoadScene("NoahTestScene");
    }

    public void QuitGameOnClick()
    {
        Application.Quit();
    }
}
