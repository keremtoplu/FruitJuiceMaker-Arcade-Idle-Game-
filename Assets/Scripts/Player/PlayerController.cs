using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Shadout.Models;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace Shadout.Controllers
{
    public class PlayerController : Singleton<PlayerController>, IPlayer, IStacker
    {
        #region SerializedFields

        [SerializeField]
        private GameObject tray;

        [SerializeField]
        private PlayerConfig playerConfig;

        [SerializeField]
        private int capacity = 5;

        [SerializeField]
        private StackController stack;

        [SerializeField, BoxGroup("Components")]
        private PlayerMovementConfig playerMovementConfig;

        [SerializeField, BoxGroup("Components")]
        private FloatingJoystick joystick;

        #endregion

        #region Variables

        private PlayerMovement playerMovement;
        private Rigidbody body;
        private Animator animator;
        private bool isPlayerOutside = false;

        #endregion

        #region Events

        public event Action IncomeLevelChanged;

        #endregion

        #region Props

        private Rigidbody Body => body ??= GetComponent<Rigidbody>();
        private Animator Animator => animator ??= GetComponent<Animator>();
        public int IncomeLevel { get; private set; }
        public StackController Stack { get => stack; set { } }
        public int Capacity { get => capacity; set { } }
        public float Speed => playerMovement.MovementSpeed;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            IncomeLevel = PlayerPrefs.HasKey(nameof(IncomeLevel)) ? PlayerPrefs.GetInt(nameof(IncomeLevel)) : playerConfig.IncomeLevel;
            playerMovement = new PlayerMovement(playerMovementConfig, Body, transform);
            stack.Setup();
            capacity = PlayerPrefs.HasKey(nameof(capacity)) ? PlayerPrefs.GetInt(nameof(capacity)) : playerConfig.Capacity;

            playerMovement.Initialize();
        }

        private void Update()
        {
            if (!isPlayerOutside || stack.IsFull) return;
            //LockAnimal();
        }

        private void FixedUpdate()
        {
            playerMovement.Update(joystick.Direction);
            MoveAnimate();

            stack.MoveStack(joystick.Direction.magnitude > 0.1f);
        }

        #endregion

        #region Methods

        public void Save()
        {
            PlayerPrefs.SetInt(nameof(IncomeLevel), IncomeLevel);
            PlayerPrefs.SetInt(nameof(capacity), capacity);
        }

        public void UpgradeIncome(int value)
        {
            IncomeLevel++;
            CurrencyController.Instance.Decrease(value);
            if (!SfxController.Instance.AllSoundsClose)
                SfxController.Instance.GetCashOutSound();
            IncomeLevelChanged?.Invoke();
            Save();
        }

        public void UpgradeCapacity(int value)
        {
            capacity += 2;
            CurrencyController.Instance.Decrease(value);
            if (!SfxController.Instance.AllSoundsClose)
                SfxController.Instance.GetCashOutSound();

            Save();
        }

        public void UpgradeSpeed(int value)
        {
            playerMovement.UpgradeSpeed();
            CurrencyController.Instance.Decrease(value);
            if (!SfxController.Instance.AllSoundsClose)
                SfxController.Instance.GetCashOutSound();
        }

        private void MoveAnimate()
        {
            Animator.SetBool("isEmpty", stack.IsEmpty);
            tray.SetActive(!stack.IsEmpty);

            Animator.SetFloat("Blend", Mathf.Clamp01(joystick.Direction.magnitude));
        }

        public void CollectFruit(TreeController tree)
        {
            FruitController fruit;
            if (tree.TryCollectFruit(out fruit) && stack.ItemCount < capacity)
            {
                stack.Add(fruit);
            }
        }

        #endregion

        #region Callbacks

        #endregion
    }
}