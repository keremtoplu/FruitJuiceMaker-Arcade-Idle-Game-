using UnityEngine;
using TMPro;
using System;

namespace Shadout.Controllers
{
	public class BuyUIController : MonoBehaviour
	{
		#region SerializedFields
		
        [SerializeField]
        private Transform animationTarget;

        [SerializeField]
        private TextMeshProUGUI costText;

		#endregion

		#region Variables

        private BuyController buyController;

		#endregion

		#region Events

		#endregion

		#region Props

		#endregion

		#region Unity Methods

		#endregion

		#region Methods

		public void Setup(BuyController buyController)
		{
			this.buyController = buyController;

			OnDataUpdated();

			buyController.DataUpdated += OnDataUpdated;
			buyController.Bought += OnBought;
            buyController.PlayerEnteredStateChanged += OnPlayerEnteredStateChanged;
		}

		#endregion

		#region Callbacks

        private void OnPlayerEnteredStateChanged(bool state)
        {
            LeanTween.cancel(animationTarget.gameObject);

            if (state)
            {
                LeanTween.scale(animationTarget.gameObject, Vector3.one * 1.2f , .1f)
                .setEaseInQuad();
            }
            else
            {
                LeanTween.scale(animationTarget.gameObject, Vector3.one, .1f)
                .setEaseInQuad();
            }
        }

        private void OnBought(BuyController buyController)
        {
            gameObject.SetActive(false);
        }

        private void OnDataUpdated()
        {
            if (buyController.CurrentCost <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                costText.SetText("$" + buyController.CurrentCost.FormatCurrency());
            }
        }

		#endregion
	}
}