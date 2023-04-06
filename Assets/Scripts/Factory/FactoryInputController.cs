using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;

public class FactoryInputController : MonoBehaviour
{
    [SerializeField]
    private FactoryController factory;

    private void OnTriggerEnter(Collider other) 
    {
        var stacker = other.GetComponent<IStacker>();
        if (stacker != null)
        {
            factory.AddToFactory(stacker.Stack);
        }
    }
}
