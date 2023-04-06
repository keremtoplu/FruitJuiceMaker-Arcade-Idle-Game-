using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnState : IState
{
    private GameObject agent;
    public DespawnState(GameObject agent)
    {
        this.agent = agent;
    }
    public void OnEnter()
    {
        SimplePool.Despawn(agent);
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}
