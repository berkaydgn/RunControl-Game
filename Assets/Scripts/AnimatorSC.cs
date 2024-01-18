using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSC : MonoBehaviour
{
    public Animator _Animator;
    public void Pacify()
    {
        _Animator.SetBool("ok", false);
    }
}
