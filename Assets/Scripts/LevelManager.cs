using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Library;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public Button[] Buttons;
    public int Level;
    public Sprite LockButton;
    public AudioSource ButtonSound;
    MemoryManagment _memoryManagment = new MemoryManagment();


    void Start()
    {
        ButtonSound.volume = _memoryManagment.ReadData_f("MenuFx");

        int validLevel = _memoryManagment.ReadData_i("LastLevel") - 4;
        int index = 1;

        for (int i = 0; i < Buttons.Length; i++) 
        {
            if (index <= validLevel)
            {
                Buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = index.ToString();
                int sceneIndex = index + 4;
                Buttons[i].onClick.AddListener(delegate { SceneLoad(sceneIndex); });

            }
            else
            {
                Buttons[i].GetComponent<Image>().sprite = LockButton;
                Buttons[i].enabled = false;
            }
            index++;
        }
    }

    public void SceneLoad(int index)
    {
       ButtonSound.Play();
       SceneManager.LoadScene(index);
    }

    public void TurnBack()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(0);
    }


}
