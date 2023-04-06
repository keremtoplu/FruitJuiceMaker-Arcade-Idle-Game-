using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shadout.Controllers
{
	public class BuyManager : Singleton<BuyManager>
	{
		#region SerializedFields

		#endregion

		#region Variables

		private Dictionary<int,List<BuyController>> buyControllers = new Dictionary<int,List<BuyController>>();
		private int currentControllerCount = 0;
		private int buyControllersLevel;
		private int count = 0;

		#endregion

		#region Events

		#endregion

		#region Props

		#endregion

		#region Unity Methods

		private void Start() 
		{
			buyControllersLevel = PlayerPrefs.HasKey(nameof(buyControllersLevel)) ? PlayerPrefs.GetInt(nameof(buyControllersLevel)) : 0;
			GetLevel(buyControllersLevel);
		}

        #endregion

        #region Methods

		
        public void Save()
        {
            PlayerPrefs.SetInt(nameof(buyControllersLevel), buyControllersLevel);
        }

        public void Add(BuyController buyController, int level)
		{
			count++;
			buyController.gameObject.SetActive(false);

			if(buyControllers.ContainsKey(level))
			{
                buyControllers[level].Add(buyController);
			}
			else
			{
				buyControllers.Add(level, new List<BuyController>());
				buyControllers[level].Add(buyController);
			}

			buyController.Index = count;
		}

		private void GetLevel(int level)
		{
			if(!buyControllers.ContainsKey(level)) return;
			
			for (int i = 0; i < buyControllers[level].Count; i++)
			{
				if (!buyControllers[level][i].IsBought)
				{
					currentControllerCount++;
					buyControllers[level][i].Bought += OnBought;
					buyControllers[level][i].gameObject.SetActive(true);
				}
			}
		}

		#endregion

		#region Callbacks

        private void OnBought(BuyController buyController)
        {
			PlayerPrefs.SetInt("buyControllerBought"+buyController.Index.ToString(), 1);
            currentControllerCount--;
			buyController.Bought -= OnBought;
			buyControllers[buyControllersLevel].Remove(buyController);

			if (currentControllerCount <= 0)
			{
				GetLevel(++buyControllersLevel);
                Save();
			}

        }

		#endregion
	}
}