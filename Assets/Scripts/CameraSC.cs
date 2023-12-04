using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSC : MonoBehaviour
{
    public Transform target;
    public Vector3 target_offset;
    public bool attackArena;
    public GameObject cameraAttackPosition;
    void Start()
    {
        target_offset = transform.position - target.position;
    }

    public void LateUpdate()
    {
        if(!attackArena) 
            transform.position = Vector3.Lerp(transform.position, target.position + target_offset, .125f);
        else
            transform.position = Vector3.Lerp(transform.position, cameraAttackPosition.transform.position, .015f);
    }
}
