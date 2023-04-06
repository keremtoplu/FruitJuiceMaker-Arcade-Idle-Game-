using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        var agent = other.GetComponent<AgentController>();
        if (agent != null)
        {
            SimplePool.Despawn(agent.gameObject);
        }
    }
}
