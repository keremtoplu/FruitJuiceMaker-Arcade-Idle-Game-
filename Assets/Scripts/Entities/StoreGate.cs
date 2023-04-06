using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;

public class StoreGate : MonoBehaviour
{
    [SerializeField]
    private Transform gateLeft;

    [SerializeField]
    private Transform gateRight;

    private void OnTriggerEnter(Collider other) {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            OpenGate();
        }

        var agent = other.gameObject.GetComponent<AgentController>();
        if (agent != null)
        {
            OpenGate();
        }
    }

    private void OnTriggerExit(Collider other) {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            CloseGate();
        }

        var agent = other.gameObject.GetComponent<AgentController>();
        if (agent != null)
        {
            CloseGate();
        }
    }

    private void OpenGate()
    {
        LeanTween.cancel(gateLeft.gameObject);
        LeanTween.moveLocalX(gateLeft.gameObject, -.8f , .5f).setEaseInSine();
        LeanTween.cancel(gateRight.gameObject);
        LeanTween.moveLocalX(gateRight.gameObject, .8f , .5f).setEaseInSine();

    }

    private void CloseGate()
    {
        LeanTween.cancel(gateLeft.gameObject);
        LeanTween.moveLocalX(gateLeft.gameObject, 0, .5f).setEaseInSine();
        LeanTween.cancel(gateRight.gameObject);
        LeanTween.moveLocalX(gateRight.gameObject, 0, .5f).setEaseInSine();
    }

}
