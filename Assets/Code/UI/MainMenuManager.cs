using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void StartGameOnClick()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGameOnClick()
    {
        Application.Quit();
    }
}
