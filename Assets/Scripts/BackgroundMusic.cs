using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float vol = PlayerPrefs.GetFloat("volume");
        GetComponent<AudioSource>().volume = vol;
    }

    void Update()
    {
        float vol = PlayerPrefs.GetFloat("volume");
        GetComponent<AudioSource>().volume = vol;
    }
}
