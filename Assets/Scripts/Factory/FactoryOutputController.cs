using System;
using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;

public class FactoryOutputController : MonoBehaviour
{
    [SerializeField]
    private FactoryController factory;

    private void OnTriggerStay(Collider other) 
    {
        var stacker = other.GetComponent<IStacker>();
        if (stacker != null)
        {
            factory.GiveJuice(stacker);
        }
    }
}
