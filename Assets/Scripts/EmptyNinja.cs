using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Progress;

public class EmptyNinja : MonoBehaviour
{
    public GameManager _GameManager;
    public SkinnedMeshRenderer _Renderer;
    public Material NewMaterial;
    public NavMeshAgent _Navmesh;
    public Animator _Animator;
    public GameObject Target;
    bool isContact;


    private void LateUpdate()
    {
        if (isContact)
            _Navmesh.SetDestination(Target.transform.position);
    }

    Vector3 NewPosition()
    {
        return new Vector3(transform.position.x, .23f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("CopyPlayer"))
        {
            if (gameObject.CompareTag("EmptyNinja"))
            {
                ChangeMaterial();
                isContact = true;
                GetComponent<AudioSource>().Play();
            }
        }

        else if (other.CompareTag("EnemyBox"))
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
    }

    void ChangeMaterial()
    {
        Material[] mats = _Renderer.materials;
        mats[0] = NewMaterial;
        _Renderer.materials = mats;
        _Animator.SetBool("Attack", true);
        gameObject.tag = "CopyPlayer";
        GameManager.PlayerCount++;
    }
}
