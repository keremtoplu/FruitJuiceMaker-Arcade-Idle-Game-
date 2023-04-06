using System;
using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using TMPro;
using UnityEngine;

public class PlayerUpgradeController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerUpgradePanel;

    [SerializeField]
    private TextMeshProUGUI currencyText;

    private void Start() 
    {
        currencyText.text = CurrencyController.Instance.Currency.ToString();

        CurrencyController.Instance.CurrencyChanged += OnCurrencyChanged;
    }


    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            playerUpgradePanel.SetActive(true);
        }
    }

    public void CloseUpgradeStore()
    {
        playerUpgradePanel.SetActive(false);
        // PlayerController.Instance.Joystick.enabled = true;
    }
    private void OnCurrencyChanged()
    {
        currencyText.text = CurrencyController.Instance.Currency.ToString();
    }
}
