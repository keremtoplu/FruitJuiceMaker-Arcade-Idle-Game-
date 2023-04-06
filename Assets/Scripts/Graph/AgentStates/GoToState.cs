using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;

public class GoToState : IState
{
    private AgentController agent;
    private Node target;
    private Animator animator;

    public GoToState(AgentController agent, Node target, Animator animator)
    {
        this.agent = agent;
        this.target = target;
        this.animator = animator;
    }

    public void OnEnter()
    {
        agent.SetTarget(target);
        animator.SetTrigger("walk");
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
