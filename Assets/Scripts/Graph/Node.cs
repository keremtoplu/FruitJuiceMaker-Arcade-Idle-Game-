using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [HideInInspector]
    public float radius;
    [HideInInspector]
    public Color color;
    public int Index;
    public bool isActive = true;

    public List<Node> OutgoingList = new List<Node>();

    public List<Node> IncomingList = new List<Node>();
    
}
