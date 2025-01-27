using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement; 
using UnityEngine.EventSystems;

public class PlayButton : MonoBehaviour
{
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
