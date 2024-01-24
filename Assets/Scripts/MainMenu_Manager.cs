using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Library;


public class MainMenu_Manager : MonoBehaviour
{
    MemoryManagment _memoryManagment = new MemoryManagment();
    DataManagment _DataManagment = new DataManagment();
    public GameObject ExitPanel;
    public List<ItemInformation> _ItemInformation = new List<ItemInformation>();
    public AudioSource ButtonSound;

    void Start()
    {
        _memoryManagment.CheckAndIdentify();
        _DataManagment.FirstSaveFile(_ItemInformation);
        ButtonSound.volume = _memoryManagment.ReadData_f("MenuFx");
    }

    public void SceneLoad(int index)
    {
        ButtonSound.Play();
        SceneManager.LoadScene(index);
    }

    public void Play()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(_memoryManagment.ReadData_i("LastLevel"));
    }

    public void ExitButtonOperation(string value)
    {
        ButtonSound.Play();
        if (value == "Exit1")
            ExitPanel.SetActive(true);
        else if (value == "Exit2")
            Application.Quit();
        else
            ExitPanel.SetActive(false);
    }

}
