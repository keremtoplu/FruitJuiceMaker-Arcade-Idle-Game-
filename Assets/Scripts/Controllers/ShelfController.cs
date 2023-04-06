using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;

public class ShelfController : MonoBehaviour
{
    [SerializeField]
    private Node node;

    [SerializeField]
    private Node waitNode;

    [SerializeField]
    private StackController stack;

    [SerializeField]
    private BuyController buyController;

    public bool IsWaitOccupied = false;

    public StackController Stack => stack;
    public Node Node => node;
    public Node WaitNode => waitNode;
    public BuyController BuyController => buyController;

    private void Start() 
    {
        
    }
}
