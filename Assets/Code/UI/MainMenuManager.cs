using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void Start()
    {
        AudioManager.instance.PlayMenuMusic();
    }
    public void StartGameOnClick()
    {
        AudioManager.instance.StopMenuMusic();
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGameOnClick()
    {
        Application.Quit();
    }
}
