using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Library;
using TMPro;
using System.Drawing;


public class Customize : MonoBehaviour
{
    public TextMeshProUGUI PointText;
    public TextMeshProUGUI HatText;
    [Header("HATS")]
    public GameObject[] Hats;
    public Button[] HatsButtons;
    [Header("BATS")]
    public GameObject[] Bats;
    [Header("MATERIALS")]
    public Material[] Materials;

    int HatIndex = -1;

    MemoryManagment _Memorymanagment = new MemoryManagment();
    DataManagment _Datamanagment = new DataManagment();

    public List<ItemInformation> _ItemInformation = new List<ItemInformation>();


    void Start()
    {
        _Memorymanagment.SaveData_i("ActiveHat", -1);

        if (_Memorymanagment.ReadData_i("ActiveHat") == -1)
        {

            foreach (var item in Hats)
            {
                item.SetActive(false);
            }
            HatIndex = -1;
            HatText.text = "No Hat";

        }
        else
        {
            HatIndex = _Memorymanagment.ReadData_i("ActiveHat");
            Hats[HatIndex].SetActive(true);
        }


        //_Datamanagment.Save(_ItemInformation);

        _Datamanagment.Load();
        _ItemInformation = _Datamanagment.TransferList();

    }

   

    public void HatDirectionalButtons(string process)
    {
        if (process == "Forward")
        {
            if (HatIndex == -1)
            {
                HatIndex = 0;
                Hats[HatIndex].SetActive(true);
                HatText.text = _ItemInformation[HatIndex].ItemName;
            }
            else
            {
                Hats[HatIndex].SetActive(false);
                HatIndex++;
                Hats[HatIndex].SetActive(true);
                HatText.text = _ItemInformation[HatIndex].ItemName;
            }

            if (HatIndex == Hats.Length-1)
                HatsButtons[1].interactable = false;
            else
                HatsButtons[1].interactable = true;
            
            if (HatIndex != -1)
                HatsButtons[0].interactable = true;
        }
        else
        {
            if (HatIndex != -1)
            {
                Hats[HatIndex].SetActive(false);
                HatIndex--;
                if (HatIndex != -1)
                {
                    Hats[HatIndex].SetActive(true);
                    HatsButtons[0].interactable = true;
                    HatText.text = _ItemInformation[HatIndex].ItemName;
                }
                else
                {
                    HatsButtons[0].interactable = false;
                    HatText.text = "No Hat";
                }
            }

            else
            {
                HatsButtons[0].interactable = false;
                HatText.text = "No Hat";
            }

            if (HatIndex != Hats.Length - 1)
                HatsButtons[1].interactable = true;

        }
    }


}
