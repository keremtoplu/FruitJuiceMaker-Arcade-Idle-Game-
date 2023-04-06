using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using Shadout.Models;
using UnityEngine;

public class CashierController : MonoBehaviour
{
    [SerializeField]
    private Transform caseTransform;

    [SerializeField]
    private Animator animator;

    private void Start()
    {
        StartCoroutine(StartRandomAnimation(Random.Range(5, 7)));
    }

    private IEnumerator StartRandomAnimation(float time)
    {
        yield return new WaitForSeconds(time);

        if (Random.value < .5f)
        {
            animator.SetTrigger("Fax");
        }
        else
        {
            animator.SetTrigger("Breathing");
        }
        StartCoroutine(StartRandomAnimation(Random.Range(5, 7)));
    }

    private void OnTriggerEnter(Collider other)
    {
        var stacker = other.GetComponent<IStacker>();
        if (stacker != null)
        {
            var counter = 0;
            while (stacker.Stack.Contains(ItemType.AppleJuice))
            {
                counter++;
                var juice = stacker.Stack.Remove(ItemType.AppleJuice);
                juice.SetTarget(caseTransform, .3f, () =>
                {
                    SimplePool.Despawn(juice.gameObject);
                    CurrencyController.Instance.Increase(juice.ItemPrice);
                });
            }
            while (stacker.Stack.Contains(ItemType.OrangeJuice))
            {
                counter++;
                var juice = stacker.Stack.Remove(ItemType.OrangeJuice);
                juice.SetTarget(caseTransform, .3f, () =>
                {
                    SimplePool.Despawn(juice.gameObject);
                    CurrencyController.Instance.Increase(juice.ItemPrice);
                });
            }
            while (stacker.Stack.Contains(ItemType.Lemonade))
            {
                counter++;
                var juice = stacker.Stack.Remove(ItemType.Lemonade);
                juice.SetTarget(caseTransform, .3f, () =>
                {
                    SimplePool.Despawn(juice.gameObject);
                    CurrencyController.Instance.Increase(juice.ItemPrice);
                });
            }
            if (counter > 0)
            {
                UIController.Instance.MoneyScaleAnimation();
                if (!SfxController.Instance.AllSoundsClose)
                    SfxController.Instance.GetCashOutSound();
            }
        }
    }
}
