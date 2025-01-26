using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public float music;
    public float sound;

    void Start()
    {
        music = PlayerPrefs.GetFloat("mVolume");
        sound = PlayerPrefs.GetFloat("sVolume");
    }

    void Update()
    {
        PlayerPrefs.SetFloat("mVolume", music);
        PlayerPrefs.SetFloat("sVolume", sound);

        if (music > 10)
        {
            music = 10;
        }

        if (music < 0)
        {
            music = 0;
        }

        if (sound > 10)
        {
            sound = 10;
        }

        if (sound < 0)
        {
            sound = 0;
        }
    }
}
