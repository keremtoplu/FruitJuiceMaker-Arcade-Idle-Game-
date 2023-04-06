using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;
using UnityEngine.UI;

public class OtomatController : MonoBehaviour
{
    [SerializeField]
    private float time;

    [SerializeField]
    private Image filler;

    [SerializeField]
    private StackController stack;

    [SerializeField]
    private GameObject dollarPrefab;

    [SerializeField]
    private int capacity;

    private float timer = 0;

    private void Start()
    {
        timer = time;
        stack.Setup();
    }

    private void Update()
    {
        if (stack.ItemCount >= capacity) return;

        timer -= Time.deltaTime;
        filler.fillAmount = 1 - timer / time;
        if (timer <= 0)
        {
            timer = time;
            var dollar = SimplePool.Spawn(dollarPrefab, transform.position, Quaternion.identity);
            stack.Add(dollar.GetComponent<Item>(), null, false, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null && stack.ItemCount > 0)
        {
            CurrencyController.Instance.Increase(stack.ItemCount * 25);
            UIController.Instance.MoneyScaleAnimation();
            if (!SfxController.Instance.AllSoundsClose)
                SfxController.Instance.GetCashOutSound();
            stack.ClearStack();
        }
    }
}
