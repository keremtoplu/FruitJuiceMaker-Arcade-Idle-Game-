using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField]
    private GameObject gate1;

    [SerializeField]
    private GameObject gate2;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.transform.position.x > transform.position.x)
            {
                OpenGate(true);
            }
            else
            {
                OpenGate(false);
            }
        }

        var agent = other.gameObject.GetComponent<AgentController>();
        if (agent != null)
        {
            if (agent.transform.position.x > transform.position.x)
            {
                OpenGate(true);
            }
            else
            {
                OpenGate(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var stacker = other.gameObject.GetComponent<IStacker>();
        if (stacker != null)
        {
            LeanTween.cancel(gate1);
            LeanTween.rotateY(gate1, 0, .4f).setEaseInOutBack();
            LeanTween.cancel(gate2);
            LeanTween.rotateY(gate2, 0, .4f).setEaseInOutBack();
        }
    }


    private void OpenGate(bool isEnter)
    {
        if (isEnter)
        {
            LeanTween.cancel(gate1);
            LeanTween.rotateY(gate1, 90, .4f).setEaseInOutBack();
            LeanTween.cancel(gate2);
            LeanTween.rotateY(gate2, -90, .4f).setEaseInOutBack();
        }
        else
        {
            LeanTween.cancel(gate1);
            LeanTween.rotateY(gate1, -90, .4f).setEaseInOutBack();
            LeanTween.cancel(gate2);
            LeanTween.rotateY(gate2, 90, .4f).setEaseInOutBack();
        }
    }
}
