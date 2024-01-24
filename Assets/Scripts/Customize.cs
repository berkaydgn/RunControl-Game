using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Library;
using TMPro;
using System.Drawing;
using System;
using System.Transactions;
using static System.Collections.Specialized.BitVector32;
using UnityEngine.SceneManagement;


public class Customize : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    public GameObject[] ActionPanels;
    public GameObject ProcessCanvas;
    public GameObject[] GeneralPanels;
    public GameObject[] ActionButtons;
    public TextMeshProUGUI BuyText;
    int activeProcessPanel;
    [Header("--------HATS")]
    public TextMeshProUGUI HatText;
    public GameObject[] Hats;
    public Button[] HatButtons;
    [Header("--------BATS")]
    public TextMeshProUGUI BatText;
    public GameObject[] Bats;
    public Button[] BatButtons;
    [Header("--------COLOURS")]
    public TextMeshProUGUI ColourText;
    public Material DefaultColour;
    public Material[] Colours;
    public Button[] ColourButtons;
    public SkinnedMeshRenderer _Renderer;

    int HatIndex = -1;
    int BatIndex = -1;
    int ColourIndex = -1;

    MemoryManagment _Memorymanagment = new MemoryManagment();
    DataManagment _Datamanagment = new DataManagment();
    [Header("--------GENERAL DATAS")]
    public List<ItemInformation> _ItemInformation = new List<ItemInformation>();

    public Animator Saved_Animator;
    public AudioSource[] Sounds;

    void Start()
    {
        CoinText.text = _Memorymanagment.ReadData_i("Coin").ToString();

        _Datamanagment.Load();
        _ItemInformation = _Datamanagment.TransferList();

        StatusCheck(0, true);
        StatusCheck(1, true);
        StatusCheck(2, true);

        foreach (var item in Sounds)
        {
            item.volume = _Memorymanagment.ReadData_f("MenuFx");
        }
    }

    public void StatusCheck(int Section, bool process=false)
    {

        if (Section == 0)
        {
            if (_Memorymanagment.ReadData_i("ActiveHat") == -1)
            {
                foreach (var item in Hats)
                {
                    item.SetActive(false);
                }

                BuyText.text = "Buy";
                ActionButtons[0].GetComponent<Button>().interactable = false;
                ActionButtons[1].GetComponent<Button>().interactable = false;

                if (!process)
                {
                    HatIndex = -1;
                    HatText.text = "No Hat";
                }
            }
            else
            {
                foreach (var item in Hats)
                {
                    item.SetActive(false);
                }

                HatIndex = _Memorymanagment.ReadData_i("ActiveHat");
                Hats[HatIndex].SetActive(true);

                HatText.text = _ItemInformation[HatIndex].ItemName;
                BuyText.text = "Buy";
                ActionButtons[0].GetComponent<Button>().interactable = false;
                ActionButtons[1].GetComponent<Button>().interactable = true;
            }
        }

        else if (Section == 1)
        {
            if (_Memorymanagment.ReadData_i("ActiveBat") == -1)
            {
                foreach (var item in Bats)
                {
                    item.SetActive(false);
                }

                ActionButtons[0].GetComponent<Button>().interactable = false;
                ActionButtons[1].GetComponent<Button>().interactable = false;
                BuyText.text = "Buy";

                if (!process)
                {
                    BatIndex = -1;
                    BatText.text = "No Bat";
                }
            }
            else
            {
                foreach (var item in Bats)
                {
                    item.SetActive(false);
                }

                BatIndex = _Memorymanagment.ReadData_i("ActiveBat");
                Bats[BatIndex].SetActive(true);

                BatText.text = _ItemInformation[BatIndex + 3].ItemName;
                BuyText.text = "Buy";
                ActionButtons[0].GetComponent<Button>().interactable = false;
                ActionButtons[1].GetComponent<Button>().interactable = true;
            }
        }

        else
        {
            if (_Memorymanagment.ReadData_i("ActiveColour") == -1)
            {
                if (!process)
                {
                    BuyText.text = "Buy";
                    ColourIndex = -1;
                    ColourText.text = "No Colour";
                    ActionButtons[0].GetComponent<Button>().interactable = false;
                    ActionButtons[1].GetComponent<Button>().interactable = false;
                }
                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = DefaultColour;
                    _Renderer.materials = mats;
                    BuyText.text = "Buy";
                }
            }
            else
            {

                //foreach (var item in Colours)
                //{
                //    item.SetActive(false);
                //}

                ColourIndex = _Memorymanagment.ReadData_i("ActiveColour");
                Material[] mats = _Renderer.materials;
                mats[0] = Colours[ColourIndex];
                _Renderer.materials = mats;

                ColourText.text = _ItemInformation[ColourIndex + 6].ItemName;
                BuyText.text = "Buy";
                ActionButtons[0].GetComponent<Button>().interactable = false;
                ActionButtons[1].GetComponent<Button>().interactable = true;
            }
        }
    } 

    public void Buy()
    {
        Sounds[1].Play();
        if (activeProcessPanel != -1)
        {
            switch (activeProcessPanel)
            {
                case 0:
                    BuyResult(HatIndex);
                    break;
                case 1:
                    int index = BatIndex + 3;
                    BuyResult(index);
                    break;
                case 2:
                    int index2 = ColourIndex + 6;
                    BuyResult(index2);
                    break;
            }
        }
    }

    public void Equip()
    {
        Sounds[2].Play();
        if (activeProcessPanel != -1)
        {
            switch (activeProcessPanel)
            {
                case 0:
                    EquipResult("ActiveHat", HatIndex);
                    break;
                case 1:
                    EquipResult("ActiveBat", BatIndex);
                    break;
                case 2:
                    EquipResult("ActiveColour", ColourIndex);
                    break;
            }
        }
    }
    
    public void HatDirectionalButtons(string process)
    {
        Sounds[0].Play();
        if (process == "Forward")
        {
            if (HatIndex == -1)
            {
                HatIndex = 0;
                Hats[HatIndex].SetActive(true);
                HatText.text = _ItemInformation[HatIndex].ItemName;

                if (!_ItemInformation[HatIndex].PurchaseStatus)
                {
                    BuyText.text = _ItemInformation[HatIndex].Score + " - Buy";
                    ActionButtons[1].GetComponent<Button>().interactable = false;

                    if (_Memorymanagment.ReadData_i("Coin") < _ItemInformation[HatIndex].Score)
                        ActionButtons[0].GetComponent<Button>().interactable = false;
                    else
                        ActionButtons[0].GetComponent<Button>().interactable = true;
                }
                else
                {
                    BuyText.text = "Buy";
                    ActionButtons[0].GetComponent<Button>().interactable = false;
                    ActionButtons[1].GetComponent<Button>().interactable = true;
                }
            }
            else
            {
                Hats[HatIndex].SetActive(false);
                HatIndex++;
                Hats[HatIndex].SetActive(true);
                HatText.text = _ItemInformation[HatIndex].ItemName;

                if (!_ItemInformation[HatIndex].PurchaseStatus)
                {
                    BuyText.text = _ItemInformation[HatIndex].Score + " - Buy";
                    ActionButtons[1].GetComponent<Button>().interactable = false;
                    if (_Memorymanagment.ReadData_i("Coin") < _ItemInformation[HatIndex].Score)
                        ActionButtons[0].GetComponent<Button>().interactable = false;
                    else
                        ActionButtons[0].GetComponent<Button>().interactable = true;
                }
                else
                {
                    BuyText.text = "Buy";
                    ActionButtons[0].GetComponent<Button>().interactable = false;
                    ActionButtons[1].GetComponent<Button>().interactable = true;
                }
            }

            if (HatIndex == Hats.Length-1)
                HatButtons[1].interactable = false;
            else
                HatButtons[1].interactable = true;
            
            if (HatIndex != -1)
                HatButtons[0].interactable = true;
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
                    HatButtons[0].interactable = true;
                    HatText.text = _ItemInformation[HatIndex].ItemName;

                    if (!_ItemInformation[HatIndex].PurchaseStatus)
                    {
                        BuyText.text = _ItemInformation[HatIndex].Score + " - Buy";
                        ActionButtons[1].GetComponent<Button>().interactable = false;
                        if (_Memorymanagment.ReadData_i("Coin") < _ItemInformation[HatIndex].Score)
                            ActionButtons[0].GetComponent<Button>().interactable = false;
                        else
                            ActionButtons[0].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        BuyText.text = "Buy";
                        ActionButtons[0].GetComponent<Button>().interactable = false;
                        ActionButtons[1].GetComponent<Button>().interactable = true;
                    }
                }
                else
                {
                    HatButtons[0].interactable = false;
                    HatText.text = "No Hat";
                    BuyText.text = "Buy";
                    ActionButtons[0].GetComponent<Button>().interactable = false;
                }
            }

            else
            {
                HatButtons[0].interactable = false;
                HatText.text = "No Hat";
                BuyText.text = "Buy";
                ActionButtons[0].GetComponent<Button>().interactable = false;
            }

            if (HatIndex != Hats.Length - 1)
                HatButtons[1].interactable = true;

        }
    }

    public void BatDirectionalButtons(string process)
    {
        Sounds[0].Play();
        if (process == "Forward")
        {
            if (BatIndex == -1)
            {
                BatIndex = 0;
                Bats[BatIndex].SetActive(true);
                BatText.text = _ItemInformation[BatIndex + 3].ItemName;

                if (!_ItemInformation[BatIndex + 3].PurchaseStatus)
                {
                    BuyText.text = _ItemInformation[BatIndex + 3].Score + " - Buy";
                    ActionButtons[1].GetComponent<Button>().interactable = false;
                    if (_Memorymanagment.ReadData_i("Coin") < _ItemInformation[BatIndex + 3].Score)
                        ActionButtons[0].GetComponent<Button>().interactable = false;
                    else
                        ActionButtons[0].GetComponent<Button>().interactable = true;
                }
                else
                {
                    BuyText.text = "Buy";
                    ActionButtons[0].GetComponent<Button>().interactable = false;
                    ActionButtons[1].GetComponent<Button>().interactable = true;
                }
            }
            else
            {
                Bats[BatIndex].SetActive(false);
                BatIndex++;
                Bats[BatIndex].SetActive(true);
                BatText.text = _ItemInformation[BatIndex + 3].ItemName;

                if (!_ItemInformation[BatIndex + 3].PurchaseStatus)
                {
                    BuyText.text = _ItemInformation[BatIndex + 3].Score + " - Buy";
                    ActionButtons[1].GetComponent<Button>().interactable = false;
                    if (_Memorymanagment.ReadData_i("Coin") < _ItemInformation[BatIndex + 3].Score)
                        ActionButtons[0].GetComponent<Button>().interactable = false;
                    else
                        ActionButtons[0].GetComponent<Button>().interactable = true;
                }
                else
                {
                    BuyText.text = "Buy";
                    ActionButtons[0].GetComponent<Button>().interactable = false;
                    ActionButtons[1].GetComponent<Button>().interactable = true;
                }
            }

            if (BatIndex == Bats.Length - 1)
                BatButtons[1].interactable = false;
            else
                BatButtons[1].interactable = true;

            if (BatIndex != -1)
                BatButtons[0].interactable = true;
        }
        else
        {
            if (BatIndex != -1)
            {
                Bats[BatIndex].SetActive(false);
                BatIndex--;
                if (BatIndex != -1)
                {
                    Bats[BatIndex].SetActive(true);
                    BatButtons[0].interactable = true;
                    BatText.text = _ItemInformation[BatIndex + 3].ItemName;

                    if (!_ItemInformation[BatIndex + 3].PurchaseStatus)
                    {
                        BuyText.text = _ItemInformation[BatIndex + 3].Score + " - Buy";
                        ActionButtons[1].GetComponent<Button>().interactable = false;
                        if (_Memorymanagment.ReadData_i("Coin") < _ItemInformation[BatIndex + 3].Score)
                            ActionButtons[0].GetComponent<Button>().interactable = false;
                        else
                            ActionButtons[0].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        BuyText.text = "Buy";
                        ActionButtons[0].GetComponent<Button>().interactable = false;
                        ActionButtons[1].GetComponent<Button>().interactable = true;
                    }
                }
                else
                {
                    BatButtons[0].interactable = false;
                    BatText.text = "No Bat";
                    BuyText.text = "Buy";
                    ActionButtons[0].GetComponent<Button>().interactable = false;
                }
            }

            else
            {
                BatButtons[0].interactable = false;
                BatText.text = "No Bat";
                BuyText.text = "Buy";
                ActionButtons[0].GetComponent<Button>().interactable = false;
            }

            if (BatIndex != Bats.Length - 1)
                BatButtons[1].interactable = true;

        }
    }

    public void ColourDirectionalButtons(string process)
    {
        Sounds[0].Play();
        if (process == "Forward")
        {
            if (ColourIndex == -1)
            {
                ColourIndex = 0;
                Material[] mats = _Renderer.materials;
                mats[0] = Colours[ColourIndex];
                _Renderer.materials = mats;

                ColourText.text = _ItemInformation[ColourIndex + 6].ItemName;

                if (!_ItemInformation[ColourIndex + 6].PurchaseStatus)
                {
                    BuyText.text = _ItemInformation[ColourIndex + 6].Score + " - Buy";
                    ActionButtons[1].GetComponent<Button>().interactable = false;
                    if (_Memorymanagment.ReadData_i("Coin") < _ItemInformation[ColourIndex + 6].Score)
                        ActionButtons[0].GetComponent<Button>().interactable = false;
                    else
                        ActionButtons[0].GetComponent<Button>().interactable = true;
                }
                else
                {
                    BuyText.text = "Buy";
                    ActionButtons[0].GetComponent<Button>().interactable = false;
                    ActionButtons[1].GetComponent<Button>().interactable = true;
                }
            }
            else
            {
                ColourIndex++;
                Material[] mats = _Renderer.materials;
                mats[0] = Colours[ColourIndex];
                _Renderer.materials = mats;

                ColourText.text = _ItemInformation[ColourIndex + 6].ItemName;

                if (!_ItemInformation[ColourIndex + 6].PurchaseStatus)
                {
                    BuyText.text = _ItemInformation[ColourIndex + 6].Score + " - Buy";
                    ActionButtons[1].GetComponent<Button>().interactable = false;
                    if (_Memorymanagment.ReadData_i("Coin") < _ItemInformation[ColourIndex + 6].Score)
                        ActionButtons[0].GetComponent<Button>().interactable = false;
                    else
                        ActionButtons[0].GetComponent<Button>().interactable = true;
                }
                else
                {
                    BuyText.text = "Buy";
                    ActionButtons[0].GetComponent<Button>().interactable = false;
                    ActionButtons[1].GetComponent<Button>().interactable = true;
                }
            }

            if (ColourIndex == Colours.Length - 1)
                ColourButtons[1].interactable = false;
            else
                ColourButtons[1].interactable = true;

            if (ColourIndex != -1)
                ColourButtons[0].interactable = true;
        }
        else
        {
            if (ColourIndex != -1)
            {
                ColourIndex--;
                if (ColourIndex != -1)
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = Colours[ColourIndex];
                    _Renderer.materials = mats;

                    ColourButtons[0].interactable = true;
                    ColourText.text = _ItemInformation[ColourIndex + 6].ItemName;

                    if (!_ItemInformation[ColourIndex + 6].PurchaseStatus)
                    {
                        BuyText.text = _ItemInformation[ColourIndex + 6].Score + " - Buy";
                        ActionButtons[1].GetComponent<Button>().interactable = false;
                        if (_Memorymanagment.ReadData_i("Coin") < _ItemInformation[ColourIndex + 6].Score)
                            ActionButtons[0].GetComponent<Button>().interactable = false;
                        else
                            ActionButtons[0].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        BuyText.text = "Buy";
                        ActionButtons[0].GetComponent<Button>().interactable = false;
                        ActionButtons[1].GetComponent<Button>().interactable = true;
                    }
                }
                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = DefaultColour;
                    _Renderer.materials = mats;

                    ColourButtons[0].interactable = false;
                    ColourText.text = "No Colour";
                    BuyText.text = "Buy";
                    ActionButtons[0].GetComponent<Button>().interactable = false;
                }
            }

            else
            {
                Material[] mats = _Renderer.materials;
                mats[0] = DefaultColour;
                _Renderer.materials = mats;

                ColourButtons[0].interactable = false;
                ColourText.text = "No Colour";
                BuyText.text = "Buy";
                ActionButtons[0].GetComponent<Button>().interactable = false;
            }

            if (ColourIndex != Colours.Length - 1)
                ColourButtons[1].interactable = true;

        }
    }

    public void ShowPanel(int index)
    {
        Sounds[0].Play();
        StatusCheck(index);
        GeneralPanels[0].SetActive(true);
        activeProcessPanel = index;
        ActionPanels[index].SetActive(true);
        GeneralPanels[1].SetActive(true); 
        ProcessCanvas.SetActive(false);
    }

    public void TurnBack()
    {
        Sounds[1].Play();
        GeneralPanels[0].SetActive(false);
        ProcessCanvas.SetActive(true);
        GeneralPanels[1].SetActive(false);
        ActionPanels[activeProcessPanel].SetActive(false);
        StatusCheck(activeProcessPanel, true);
        activeProcessPanel = -1;
    }
    
    public void ReturnMainMenu()
    {
        Sounds[0].Play();
        _Datamanagment.Save(_ItemInformation);
        SceneManager.LoadScene(0);
    }


    //---------------------------------------
    public void BuyResult(int index)
    {
        _ItemInformation[index].PurchaseStatus = true;
        _Memorymanagment.SaveData_i("Coin", _Memorymanagment.ReadData_i("Coin") - _ItemInformation[index].Score);
        BuyText.text = "Buy";
        ActionButtons[0].GetComponent<Button>().interactable = false;
        ActionButtons[1].GetComponent<Button>().interactable = true;
        CoinText.text = _Memorymanagment.ReadData_i("Coin").ToString();
    }
    public void EquipResult(string Active, int Index)
    {
        _Memorymanagment.SaveData_i(Active, Index);
        ActionButtons[1].GetComponent<Button>().interactable = false;
        if (!Saved_Animator.GetBool("ok"))
            Saved_Animator.SetBool("ok", true);
    }

}
