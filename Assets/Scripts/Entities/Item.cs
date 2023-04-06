using System;
using System.Collections.Generic;
using Shadout.Models;
using UnityEngine;

namespace Shadout.Controllers
{
	public class Item : MonoBehaviour
	{
		#region SerializedFields

        [SerializeField]
        private ItemConfig itemConfig;

        private int itemPrice;
		private float animTime = .5f;

		#endregion

		#region Variables

        private const string ItemModelPath = "Prefabs/Items/";

        private Action onItemAnimCompleted;

        private Transform transformToAnimate;

        public static Vector3 ItemScale = Vector3.one;

        private static Dictionary<string, GameObject> itemPrefabDict = new Dictionary<string, GameObject>();

		#endregion

		#region Events

		#endregion

		#region Props

        public int ItemPrice => itemPrice;
        public ItemType ItemType => itemConfig.ItemType;

		#endregion

		#region Unity Methods

        private void Awake() 
        {
            itemPrice = PlayerController.Instance.IncomeLevel * itemConfig.Price;
            PlayerController.Instance.IncomeLevelChanged += OnIncomeLevelChanged;
        }

        #endregion

        #region Methods

        public Item SetTarget(Transform toTransform, float animTime, Action onItemAnimCompleted = null, bool isWithY = true)
        {
            this.transformToAnimate     = toTransform;
            this.onItemAnimCompleted    = onItemAnimCompleted;

            // AnimationCurve animationCurve;
            // if(transform.position.y > toTransform.position.y)
            // {
            //     animationCurve = AnimatorSettings.Instance.ItemAnimationCurve;
            // }
            // else
            // {
            //     animationCurve = AnimatorSettings.Instance.ItemAnimationCurveReverse;
            // }

            LeanTween.cancel(gameObject);
            LeanTween.move(gameObject, toTransform, animTime)
            .setOnComplete(() => OnComplete());
            if (isWithY)
            {
                LeanTween.moveY(gameObject, toTransform.position.y, animTime);//.setEase(animationCurve);
            }
            gameObject.SetActive(true);

            return this;
        }

        public Item WithRotation(bool isStackHolder, float time = -1)
        {
            //var animTime = AnimatorSettings.Instance.DefaultItemAnimTime;
            if(time != -1)
            {
                animTime = time;
            }
            //var animationCurve = AnimatorSettings.Instance.ItemAnimationCurve;
            if (isStackHolder)
            {
                var targetRotation = Vector3.zero;//new Vector3(itemPrefab.transform.eulerAngles.x, transformToAnimate.eulerAngles.y, itemPrefab.transform.eulerAngles.z);
                LeanTween.rotate(gameObject, Vector3.zero/*itemPrefab.transform.eulerAngles*/, animTime);
                LeanTween.delayedCall(animTime, ()=> transform.localRotation = Quaternion.identity/*itemPrefab.transform.rotation*/);
            }
            else
            {
                LeanTween.rotate(gameObject, transformToAnimate.rotation.eulerAngles, animTime);
            }

            return this;
        }

		#endregion

		#region Callbacks

        private void OnComplete()
        {
            transform.localScale = ItemScale;

            onItemAnimCompleted?.Invoke();

            LeanTween.cancel(gameObject);
        }

        private void OnIncomeLevelChanged()
        {
            itemPrice = PlayerController.Instance.IncomeLevel * itemConfig.Price;
        }

		#endregion
	}
}