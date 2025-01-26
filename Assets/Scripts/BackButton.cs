using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public void OnPointerDown(PointerEventData eventData)
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}