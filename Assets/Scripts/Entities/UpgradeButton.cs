using System;
using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField]
    private int value;

    [SerializeField]
    private UpgradeType upgradeType;

    private Button button;

    [SerializeField]
    private TextMeshProUGUI price;

    private void Start() 
    {
        button = GetComponent<Button>();
        button.interactable = CurrencyController.Instance.Currency >= value;

        price.text = "$"+value.ToString();

        CurrencyController.Instance.CurrencyChanged += OnCurrencyChanged;
    }

    private void OnCurrencyChanged()
    {
        button.interactable = CurrencyController.Instance.Currency >= value;
    }
}
