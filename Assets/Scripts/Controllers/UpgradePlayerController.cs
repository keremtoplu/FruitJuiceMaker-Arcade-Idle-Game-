using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;

public class UpgradePlayerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            UIController.Instance.OpenUpgradePlayerPanel();
        }
    }
}
