using System;
using Shadout.Models;
using UnityEngine;

namespace Shadout.Controllers
{
	public class CurrencyController : Singleton<CurrencyController>
	{
		#region SerializedFields

		[SerializeField]
		private CurrencyConfig currencyContainer;

		#endregion

		#region Variables

		#endregion

		#region Events

		public event Action CurrencyChanged;

		#endregion

		#region Props

		public int Currency { get; private set; }

        #endregion

        #region Unity Methods

        #endregion

        #region Methods

		private void Awake() 
		{
			Initialize();
		}

		public void Initialize()
		{
			Currency = PlayerPrefs.HasKey(nameof(Currency)) ? PlayerPrefs.GetInt(nameof(Currency)) : currencyContainer.MainCurrency;
		}

        public void Save()
        {
            PlayerPrefs.SetInt(nameof(Currency), Currency);
        }

		public void Increase(int value)
		{
			Currency += value;
			Save();
			CurrencyChanged?.Invoke();
		}

		public bool Decrease(int value)
		{
			if(Currency - value < 0)
			{
				return false;
			}
			else
			{
				Currency -= value;
				Save();
                CurrencyChanged?.Invoke();
				return true;
			}
		}

        #endregion

        #region Callbacks

        #endregion
    }
}