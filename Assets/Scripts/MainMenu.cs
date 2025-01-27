using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement; 
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public GameObject controlsMenu;

    public void openSettings()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }

    public void openCredits()
    {
        creditsMenu.SetActive(!creditsMenu.activeSelf);
        controlsMenu.SetActive(!controlsMenu.activeSelf);

    }

    public void loadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
#if UNITY_EDITOR
        // Stop playing in the Unity Editor
        EditorApplication.isPlaying = false;
#else
        // Quit the application in a built game
        Application.Quit();
#endif
    }
}
