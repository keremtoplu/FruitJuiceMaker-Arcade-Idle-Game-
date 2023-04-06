using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;

public class GiveMoneyState : IState
{
    private AgentController agent;
    private CashierController cashier;

    public GiveMoneyState(AgentController agent, CashierController cashier)
    {
        this.agent = agent;
        this.cashier = cashier;
    }

    public void OnEnter()
    {
        
        while(!agent.Stack.IsEmpty)
        {
            var item = agent.Stack.Remove();
            CurrencyController.Instance.Increase(item.ItemPrice);
        }
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
    }
}
