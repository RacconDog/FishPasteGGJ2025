using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.EventSystems;

public class PlayButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject game;
    public GameObject Menu;

    public void OnPointerDown(PointerEventData eventData)
    {
        game.SetActive(true);
        Menu.SetActive(false);
    }
}
