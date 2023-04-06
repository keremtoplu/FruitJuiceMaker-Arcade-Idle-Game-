using System;
using System.Collections;
using Newtonsoft.Json;
using Shadout.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Shadout.Controllers
{
    public class BuyController : MonoBehaviour
    {
        #region SerializedFields

        [SerializeField]
        private GameObject visual;

        [SerializeField]
        private GameObject lockedVisual;

        [SerializeField]
        private int cost;

        [SerializeField]
        private int level;

        [SerializeField]
        private bool isBought = false;

        [SerializeField]
        private BuyUIController buyUIController;

        #endregion

        #region Variables
        private CountdownHelper countdown;
        private int currentCost;

        [HideInInspector]
        public int Index = 0;

        #endregion

        #region Events

        public event Action<BuyController> Bought;

        public event Action DataUpdated;

        public event Action<bool> PlayerEnteredStateChanged;

        #endregion

        #region Props

        public int CurrentCost => currentCost;
        public bool IsBought => isBought;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Setup();
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                countdown.Interval = 1f / (float)cost;
                countdown.StartCounting();

                PlayerEnteredStateChanged?.Invoke(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                countdown.StopCounting();
                PlayerEnteredStateChanged?.Invoke(false);
            }
        }

        #endregion

        #region Methods

        private void Setup()
        {
            BuyManager.Instance.Add(this, level);
            currentCost = cost;
            countdown = GetComponent<CountdownHelper>();
            countdown.Notified += OnNotified;

            if (!isBought)
            {
                isBought = PlayerPrefs.HasKey("buyControllerBought" + Index.ToString()) ? PlayerPrefs.GetInt("buyControllerBought" + Index.ToString()) == 1 : false;
            }

            currentCost = PlayerPrefs.HasKey("buyControllerValue" + Index.ToString()) ? PlayerPrefs.GetInt("buyControllerValue" + Index.ToString()) : cost;

            buyUIController.Setup(this);
            visual.SetActive(isBought);
            lockedVisual.SetActive(!isBought);
        }

        #endregion

        #region Callbacks

        private void OnNotified(int amount)
        {
            if (isBought == false)
            {
                if (currentCost > 0)
                {
                    var min = (int)Math.Min(cost, Math.Min(currentCost, amount));
                    if (CurrencyController.Instance.Decrease(min))
                    {
                        currentCost -= (int)min;
                        if (!SfxController.Instance.AllSoundsClose)
                            SfxController.Instance.GetCashOutSound();
                        DataUpdated?.Invoke();
                    }
                    PlayerPrefs.SetInt("buyControllerValue" + Index.ToString(), (int)currentCost);
                }
                else
                {
                    isBought = true;
                }
            }

            if (isBought)
            {
                Bought?.Invoke(this);

                countdown.StopCounting();
                countdown.Notified -= OnNotified;

                gameObject.SetActive(false);

                visual.transform.localScale = Vector3.one * .01f;
                LeanTween.scale(visual, Vector3.one, .3f).setEaseInOutBack();
                visual.SetActive(isBought);
                lockedVisual.SetActive(!isBought);
            }
        }

        #endregion
    }
}