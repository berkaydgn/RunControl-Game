using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class CopyPlayer : MonoBehaviour
{
    public GameManager _GameManager;
    public GameObject Target;
    NavMeshAgent NavMesh;

    void Start()
    {
        NavMesh = GetComponent<NavMeshAgent>();
    }

    void LateUpdate()
    {
        NavMesh.SetDestination(Target.transform.position);
    }
    
    Vector3 NewPosition()
    {
        return new Vector3(transform.position.x, .23f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBox"))
        {
            _GameManager.ExtinctionEffect(NewPosition());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Shredder"))
        {
            _GameManager.ExtinctionEffect(NewPosition());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("PropellerNeedles"))
        {
            _GameManager.ExtinctionEffect(NewPosition());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Sledgehammer"))
        {
            _GameManager.ExtinctionEffect(NewPosition(), true);
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Enemy"))
        {
            _GameManager.ExtinctionEffect(NewPosition(), false, false);
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("EmptyNinja"))
        {
            _GameManager.CopyPlayers.Add(other.gameObject);
        }

    }
}
