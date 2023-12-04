using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public Animator _Animator;
    public float StandbyTime;
    public BoxCollider Wind;

    public void PropellerAnimation(string situation)
    {
        if (situation == "true")
        {
            _Animator.SetBool("Play", true);
            Wind.enabled = true;
        }
        else
        {
            _Animator.SetBool("Play", false);
            StartCoroutine(TriggerAnimation());
            Wind.enabled = false;
        }

    }

    IEnumerator TriggerAnimation()
    {

        yield return new WaitForSeconds(StandbyTime);
        PropellerAnimation("true");
    }
}
