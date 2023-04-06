using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI currentCoinText;


    private void Start()
    {
        //ScoreManager.Instance.CoinChanged += OnCoinChanged;
        //currentCoinText.text = ScoreManager.Instance.CurrentCoin.ToString();
    }

    private void OnCoinChanged()
    {
        //currentCoinText.text = ScoreManager.Instance.CurrentCoin.ToString();
    }
}
