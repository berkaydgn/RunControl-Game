using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStain : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

}
    