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

    void Start()
    {
        _memoryManagment.CheckAndIdentify();
        _DataManagment.FirstSaveFile(_ItemInformation);
    }

    public void SceneLoad(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Play()
    {
        SceneManager.LoadScene(_memoryManagment.ReadData_i("LastLevel"));
    }

    public void ExitButtonOperation(string value)
    {
        if (value == "Exit1")
            ExitPanel.SetActive(true);
        else if (value == "Exit2")
            Application.Quit();
        else
            ExitPanel.SetActive(false);
    }

}
