using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;

public class TakeJuiceState : IState
{
    private List<ShelfController> shelfs;
    private AgentController agent;
    private ShelfController targetShelf;

    public TakeJuiceState(List<ShelfController> shelfs, AgentController agent, ShelfController targetShelf)
    {
        this.shelfs = shelfs;
        this.agent = agent;
        this.targetShelf = targetShelf;
    }

    public void OnEnter()
    {
        while(!targetShelf.Stack.IsEmpty && agent.Stack.ItemCount < agent.Capacity)
        {
            agent.Stack.Add(targetShelf.Stack.Remove());
        }
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
