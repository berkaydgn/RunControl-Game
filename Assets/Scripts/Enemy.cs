using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameManager _GameManager;
    public GameObject attackTarget;
    public NavMeshAgent _NavMesh;
    public Animator _Animator;
    bool didTheAttackStart;


    public void AnimationTrigger()
    {
        _Animator.SetBool("Attack", true);
        didTheAttackStart = true;
    }

    private void LateUpdate()
    {
        if(didTheAttackStart)
            _NavMesh.SetDestination(attackTarget.transform.position);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("CopyPlayer"))
        {
            _GameManager.ExtinctionEffect(Vector3.zero, false, true);
            gameObject.SetActive(false);
        }
    }
}
