using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Library;
using UnityEngine.UIElements;
using TMPro;
//using System.Diagnostics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int PlayerCount = 1; 
    public List<GameObject> CopyPlayers;
    public List<GameObject> FormationEffects;
    public List<GameObject> ExtinctionEffects;
    public List<GameObject> PlayerStainEffects;
    bool attackArena;

    [Header("LEVEL DATAS")]
    public List<GameObject> Enemies;
    public int HowManyEnemies;
    public GameObject Player;
    public bool IsTheLvlOver;
    [Header("--------HATS")]
    public GameObject[] Hats;
    [Header("--------BATS")]
    public GameObject[] Bats;
    [Header("--------COLOURS")]
    public Material[] Colours;
    public SkinnedMeshRenderer _Renderer;
    public Material DefaultColour;

    MathLibrary _mathLibrary = new MathLibrary();
    MemoryManagment _memoryManagment = new MemoryManagment();

    Scene _Scene;
    [Header("--------GENERAL DATAS")]
    public AudioSource[] Sounds;
    public GameObject[] ActionPanels;
    public UnityEngine.UI.Slider GameSoundSlider;

    private void Awake()
    {
        Sounds[0].volume = _memoryManagment.ReadData_f("GameSound");
        GameSoundSlider.value = _memoryManagment.ReadData_f("GameSound");
        Sounds[1].volume = _memoryManagment.ReadData_f("MenuFx");
        Destroy(GameObject.FindWithTag("MenuSound"));
        ItemCheck();
    }

    private void Start()
    {
        EnemiesCreate();
        _Scene = SceneManager.GetActiveScene();
    
    }


    public void EnemiesCreate()
    {
        for (int i = 0; i < HowManyEnemies; i++)
        {
            Enemies[i].SetActive(true);
        }
    }

    public void CopyPlayerManager(string operationType, int inComingNumber, Transform position) 
    {
        switch(operationType)
        {
            case "Impact":
                _mathLibrary.Impact(inComingNumber, CopyPlayers, position, FormationEffects);
                break;

            case "Collection":
                _mathLibrary.Collection(inComingNumber, CopyPlayers, position, FormationEffects);
                break;

            case "Extraction":
                _mathLibrary.Extraction(inComingNumber, CopyPlayers, ExtinctionEffects);
                break;

            case "Divide":
                _mathLibrary.Divide(inComingNumber, CopyPlayers, ExtinctionEffects);
                break;
        }
    }

    public void ExtinctionEffect(Vector3 position, bool Sledgehammer = false, bool condition = false)
    {
        foreach(var item in ExtinctionEffects)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = position;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();
                if (!condition)
                {
                    PlayerCount--;
                }
                else
                {
                    HowManyEnemies--;
                }
                
                break;
            }
        }

        if (Sledgehammer)
        {
            foreach (var item in PlayerStainEffects)
            {
                if (!item.activeInHierarchy)
                {
                    Vector3 newPoz = new Vector3(position.x, 0.001f, position.z);
                    item.SetActive(true);
                    item.transform.position = newPoz;
                    break;
                }
            }
        }

        if (!IsTheLvlOver)
        {
            FightSituation();
        }
    }

    public void EnemiesTrigger()
    {
        foreach (var item in Enemies)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Enemy>().AnimationTrigger();
            }
        }

        attackArena = true;
        FightSituation();
    }

    void FightSituation()
    {
        if (attackArena)
        {
            if (PlayerCount == 1 || HowManyEnemies == 0)
            {
                IsTheLvlOver = true;
                foreach (var item in Enemies)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Attack", false);
                    }
                }
                foreach (var item in CopyPlayers)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Attack", false);
                    }
                }

                Player.GetComponent<Animator>().SetBool("Attack", false);

                if (PlayerCount < HowManyEnemies || PlayerCount == HowManyEnemies)
                {
                    Debug.Log("Kaybettin");
                }
                else
                {
                    if (PlayerCount > 5)
                    {
                        if (_Scene.buildIndex == _memoryManagment.ReadData_i("LastLevel"))
                        {
                            _memoryManagment.SaveData_i("puan", _memoryManagment.ReadData_i("puan") + 600);
                            _memoryManagment.SaveData_i("LastLevel", _memoryManagment.ReadData_i("LastLevel") + 1);
                        }
                            
                    }
                    else
                        if (_Scene.buildIndex == _memoryManagment.ReadData_i("LastLevel"))
                        {
                        _memoryManagment.SaveData_i("puan", _memoryManagment.ReadData_i("puan") + 200);
                        _memoryManagment.SaveData_i("LastLevel", _memoryManagment.ReadData_i("LastLevel") + 1);
                        }


                    Debug.Log("Kazandin");

                }

            }
        }
    }

    public void ItemCheck()
    {
        if (_memoryManagment.ReadData_i("ActiveHat") != -1)
            Hats[_memoryManagment.ReadData_i("ActiveHat")].SetActive(true);

        if (_memoryManagment.ReadData_i("ActiveBat") != -1)
            Bats[_memoryManagment.ReadData_i("ActiveBat")].SetActive(true);

        if (_memoryManagment.ReadData_i("ActiveColour") != -1)
        {
            Material[] mats = _Renderer.materials;
            mats[0] = Colours[_memoryManagment.ReadData_i("ActiveColour")];
            _Renderer.materials = mats;
        }
        else
        {
            Material[] mats = _Renderer.materials;
            mats[0] = DefaultColour;
            _Renderer.materials = mats;
        }

    }

    public void ExitButtonOperation(string value)
    {
        Sounds[1].Play();

        if (value == "Stop")
        {
            Time.timeScale = 0;
            ActionPanels[0].SetActive(true);
        }
        else if (value == "Continue")
        {
            ActionPanels[0].SetActive(false);
            Time.timeScale = 1;
        }
        else if (value == "Replay")
        {
            SceneManager.LoadScene(_Scene.buildIndex);
            Time.timeScale = 1;
        }
        else if(value == "MainMenu")
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }

    public void Settings(string value)
    {
        if (value == "Settings")
        {
            ActionPanels[1].SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            ActionPanels[1].SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void SoundSetting()
    {
        _memoryManagment.SaveData_f("GameSound", GameSoundSlider.value);
        Sounds[0].volume = GameSoundSlider.value;
    }

}
