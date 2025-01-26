using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class VolumeButtons : MonoBehaviour, IPointerDownHandler
{
    public bool sfxUp;
    public bool sfxDown;
    public bool musicUP;
    public bool musicDown;

    public float amount;
    public float speed;
    private float startPosition;

    public GameObject optionsGO;
    private Options data;


    void Start()
    {
        startPosition = transform.position.y;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Options data = optionsGO.GetComponent<Options>();
        
        if (sfxUp == true)
        {
            data.sound = data.sound + 1;
        }

        if (sfxDown == true)
        {
            data.sound = data.sound - 1;
        }

        if (musicUP == true)
        {
            data.music = data.music + 1;
        }

        if (musicDown == true)
        {
            data.music = data.music - 1;
        }
    }


    void Update()
    {
        gameObject.transform.position = new Vector3(
            transform.position.x,
            (Mathf.Sin(Time.time * speed) * amount) + startPosition,
            transform.position.z);
    }

}
