using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Library
{
    public class MathLibrary
    {
        public void Impact(int inComingNumber, List<GameObject> CopyPlayers, Transform position, List<GameObject> FormationEffects)
        {
            int numberOfLoops = (GameManager.PlayerCount * inComingNumber) - GameManager.PlayerCount;
            int number = 0;

            foreach (var item in CopyPlayers)
            {
                if (number < numberOfLoops)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in FormationEffects)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                Vector3 newpoz = new Vector3(position.position.x, 0.2f, position.position.z);
                                item2.SetActive(true);
                                item2.transform.position = newpoz;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }

                        item.transform.position = position.position;
                        item.SetActive(true);
                        number++;
                    }

                }
                else
                {
                    number = 0;
                    break;
                }

            }
            GameManager.PlayerCount *= inComingNumber;
        }

        public void Collection(int inComingNumber, List<GameObject> CopyPlayers, Transform position, List<GameObject> FormationEffects)
        {
            int number2 = 0;
            foreach (var item in CopyPlayers)
            {

                if (number2 < inComingNumber)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in FormationEffects)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                Vector3 newpoz = new Vector3(position.position.x, 0.2f, position.position.z);
                                item2.SetActive(true);
                                item2.transform.position = newpoz;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }

                        item.transform.position = position.position;
                        item.SetActive(true);
                        number2++;
                    }

                }
                else
                {
                    number2 = 0;
                    break;
                }

            }
            GameManager.PlayerCount += inComingNumber;
        }

        public void Extraction(int inComingNumber, List<GameObject> CopyPlayers, List<GameObject> ExtinctionEffects)
        {

            if (GameManager.PlayerCount < inComingNumber)
            {

                foreach (var item in CopyPlayers)
                {

                    foreach(var item2 in ExtinctionEffects)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            item2.SetActive(true);
                            item2.transform.position = item.transform.position;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    } 


                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.PlayerCount = 1;

            }

            else
            {
                int number3 = 0;
                foreach (var item in CopyPlayers)
                {

                    if (number3 != inComingNumber)
                    {
                        if (item.activeInHierarchy)
                        {

                            foreach (var item2 in ExtinctionEffects)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    item2.SetActive(true);
                                    item2.transform.position = item.transform.position;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    break;
                                }
                            }

                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            number3++;
                        }

                    }
                    else
                    {
                        number3 = 0;
                        break;
                    }
                }
                GameManager.PlayerCount -= inComingNumber;

            }

        }

        public void Divide(int inComingNumber, List<GameObject> CopyPlayers, List<GameObject> ExtinctionEffects)
        {
            if (GameManager.PlayerCount <= inComingNumber)
            {
                foreach (var item in CopyPlayers)
                {

                    foreach (var item2 in ExtinctionEffects)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            item2.SetActive(true);
                            item2.transform.position = item.transform.position;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }

                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.PlayerCount = 1;

            }

            else
            {
                int dividing = GameManager.PlayerCount / inComingNumber;

                int number4 = 0;
                foreach (var item in CopyPlayers)
                {

                    if (number4 != dividing)
                    {
                        if (item.activeInHierarchy)
                        {

                            foreach (var item2 in ExtinctionEffects)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    item2.SetActive(true);
                                    item2.transform.position = item.transform.position;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    break;
                                }
                            }

                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            number4++;
                        }

                    }
                    else
                    {
                        number4 = 0;
                        break;
                    }
                }

                if (GameManager.PlayerCount % inComingNumber == 0)
                {
                    GameManager.PlayerCount /= inComingNumber;
                }
                else if(GameManager.PlayerCount % inComingNumber == 1)
                {
                    GameManager.PlayerCount /= inComingNumber;
                    GameManager.PlayerCount++;
                }
                else if (GameManager.PlayerCount % inComingNumber == 2)
                {
                    GameManager.PlayerCount /= inComingNumber;
                    GameManager.PlayerCount += 2;
                }

            }
        }

    }

    public class MemoryManagment
    {
        public void SaveData_s(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        public void SaveData_i(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public void SaveData_f(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }


        public string ReadData_s(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public int ReadData_i(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public float ReadData_f(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public void CheckAndIdentify()
        {
            if (!PlayerPrefs.HasKey("LastLevel"))
            {
                PlayerPrefs.SetInt("LastLevel", 5);
                PlayerPrefs.SetInt("Coin", 100);
                PlayerPrefs.SetInt("ActiveHat", -1);
                PlayerPrefs.SetInt("ActiveBat", -1);
                PlayerPrefs.SetInt("ActiveColour", -1);
            }
        }



    }

    [Serializable]
    public class ItemInformation
    {
        public int GroupIndex;
        public int ItemIndex;
        public string ItemName;
        public int Score;
        public bool PurchaseStatus;
    }

    public class DataManagment
    {
        public void Save(List<ItemInformation> _ItemInformation)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemData.gd");
            bf.Serialize(file, _ItemInformation);
            file.Close();
        }
        
        List<ItemInformation> _ItemInformation2;
        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemData.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemData.gd", FileMode.Open);
                _ItemInformation2 = (List<ItemInformation>)bf.Deserialize(file);
                file.Close();
            }
        }

        public List<ItemInformation> TransferList()
        {
            return _ItemInformation2;
        }

        public void FirstSaveFile(List<ItemInformation> _ItemInformation)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemData.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemData.gd");
                bf.Serialize(file, _ItemInformation);
                file.Close();
            }
        }
    }

}
