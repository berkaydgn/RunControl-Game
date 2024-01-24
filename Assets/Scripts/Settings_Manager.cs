using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Library;

public class Settings_Manager : MonoBehaviour
{
    public AudioSource ButtonSound;
    public Slider MenuSound;
    public Slider MenuFx;
    public Slider GameSound;
    MemoryManagment _MemoryManagment = new MemoryManagment();

    void Start()
    {
        ButtonSound.volume = _MemoryManagment.ReadData_f("MenuFx");

        MenuSound.value = _MemoryManagment.ReadData_f("MenuSound");
        MenuFx.value = _MemoryManagment.ReadData_f("MenuFx");
        GameSound.value = _MemoryManagment.ReadData_f("GameSound");
    }

    void Update()
    {
        
    }

    public void TurnBack()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(0);
    }

    public void Settings(string type)
    {
        switch (type)
        {
            case "MenuSound":
                _MemoryManagment.SaveData_f("MenuSound", MenuSound.value);
                break;

            case "MenuFx":
                _MemoryManagment.SaveData_f("MenuFx", MenuFx.value);
                break;

            case "GameSound":
                _MemoryManagment.SaveData_f("GameSound", GameSound.value);
                break;

        }


    }

    public void ChangeLanguage()
    {
        ButtonSound.Play();

    }

}
