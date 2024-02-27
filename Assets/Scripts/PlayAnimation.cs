using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public Animator animator;
    public AnimationClip clip;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        BoxFall();
    }

    void BoxFall()
    {
        animator.Play("clip");
    }
}
