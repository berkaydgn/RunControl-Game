using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Library;
using UnityEngine.UIElements;

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

    private void Start()
    {
        EnemiesCreate();
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
                MathLibrary.Impact(inComingNumber, CopyPlayers, position, FormationEffects);
                break;

            case "Collection":
                MathLibrary.Collection(inComingNumber, CopyPlayers, position, FormationEffects);
                break;

            case "Extraction":
                MathLibrary.Extraction(inComingNumber, CopyPlayers, ExtinctionEffects);
                break;

            case "Divide":
                MathLibrary.Divide(inComingNumber, CopyPlayers, ExtinctionEffects);
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
                    Debug.Log("Kazandin");
                }

            }
        }
    }


}
