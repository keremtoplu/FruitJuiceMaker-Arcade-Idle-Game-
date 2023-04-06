using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace Shadout.Controllers
{

    public class UIController : Singleton<UIController>
    {
        #region SerializedFields

        [SerializeField]
        private TextMeshProUGUI currencyText;
        [SerializeField]
        private GameObject machineUpgradePanel;
        [SerializeField]
        private GameObject playerUpgradePanel;
        [SerializeField]
        private Material material;

        [SerializeField]
        private RectTransform moneyIcon;

        [SerializeField]
        private GameObject settingsPanel;

        #endregion

        #region Variables
        private int btnPrize;
        public bool IsUIActive = false;
        private float counter = 1;

        #endregion

        #region Events

        #endregion

        #region Props
        #endregion

        #region Unity Methods

        private void Start()
        {
            currencyText.text = CurrencyController.Instance.Currency.FormatCurrency();
            CurrencyController.Instance.CurrencyChanged += OnCurrencyChanged;

            machineUpgradePanel.SetActive(false);
            playerUpgradePanel.SetActive(false);
            settingsPanel.SetActive(false);
        }

        private void Update()
        {
            counter -= Time.deltaTime * .5f;

            if (counter <= 0)
            {
                counter = 1;
            }
            material.SetTextureOffset("_MainTex", new Vector2(0, counter));
        }

        private void OnCurrencyChanged()
        {
            currencyText.text = CurrencyController.Instance.Currency.FormatCurrency();
        }

        #endregion

        #region Methods

        public void ClosePanels()
        {
            playerUpgradePanel.SetActive(false);
            machineUpgradePanel.SetActive(false);
            IsUIActive = false;
        }

        public void MoneyScaleAnimation()
        {
            LeanTween.scale(moneyIcon, Vector3.one * 1.3f, .2f).setEaseInOutCubic().setLoopPingPong(2);
        }

        private void OnItemCountChanged(int newCount)
        {
        }

        public void OpenUpgradeMachinePanel()
        {
            machineUpgradePanel.SetActive(true);
            IsUIActive = true;
        }
        public void OpenUpgradePlayerPanel()
        {
            IsUIActive = true;

            playerUpgradePanel.SetActive(true);
        }

        public void SetDevelopmentMode()
        {
            CurrencyController.Instance.Increase(100000);
        }

        public void CloseSettingsButton()
        {
            settingsPanel.SetActive(false);
        }
        public void OpenSettingsButton()
        {
            settingsPanel.SetActive(true);
        }
        #endregion

        #region Callbacks

        #endregion
    }
}