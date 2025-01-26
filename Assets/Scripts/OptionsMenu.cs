using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour, IPointerDownHandler
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public void OnPointerDown(PointerEventData eventData)
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
}
