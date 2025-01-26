using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsDisplay : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private Options data;
    public GameObject optionsGO;

    public bool music;
    public bool sound;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            Options data = optionsGO.GetComponent<Options>();
            textMesh = GetComponent<TextMeshProUGUI>();

        if (sound == true)
        {
            textMesh.text = data.sound.ToString();
        }

        if (music == true)
        {
            textMesh.text = data.music.ToString();
        }
    }
}
