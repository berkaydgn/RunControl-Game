using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CopyPlayer"))
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(-5,0,0), ForceMode.Impulse);
        }
    }
}
