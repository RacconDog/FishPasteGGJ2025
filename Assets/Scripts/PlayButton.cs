using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.EventSystems;

public class PlayButton : MonoBehaviour
{
    public void loadScene()
    {
        SceneManager.LoadScene(1);
    }
}
