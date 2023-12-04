using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmptyNinja : MonoBehaviour
{
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("CopyPlayer"))
        {
            ChangeMaterial();
            isContact = true;
        }
    }

    void ChangeMaterial()
    {
        Material[] mats = _Renderer.materials;
        mats[0] = NewMaterial;
        _Renderer.materials = mats;
        _Animator.SetBool("Attack", true);
    }
}
