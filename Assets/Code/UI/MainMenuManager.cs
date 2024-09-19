using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] SceneAsset _scene;
    private string sceneName;

    private void Start()
    {
        sceneName = _scene.name;
    }

    public void StartGameOnClick()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGameOnClick()
    {
        Application.Quit();
    }
}
