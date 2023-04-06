using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitItemState : IState
{
    private Animator animator;

    public WaitItemState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        animator.SetTrigger("idle");
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
