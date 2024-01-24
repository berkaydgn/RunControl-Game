using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    private static GameObject instance;
    
    AudioSource Sound;

    private void Start()
    {
        Sound = GetComponent<AudioSource>();

        Sound.volume = PlayerPrefs.GetFloat("MenuSound");
        DontDestroyOnLoad(Sound);

        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        Sound.volume = PlayerPrefs.GetFloat("MenuSound");
    }

}
