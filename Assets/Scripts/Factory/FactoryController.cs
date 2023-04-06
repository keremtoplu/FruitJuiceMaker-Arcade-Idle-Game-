using System;
using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using Shadout.Models;
using UnityEngine;

public class FactoryController : MonoBehaviour
{


    [SerializeField]
    private StackController inputStack;

    [SerializeField]
    private StackController outputStack;

    [SerializeField]
    private int machineCapacity;

    [SerializeField]
    private FactoryOutputController outputController;

    [SerializeField]
    private Transform machineTransform;

    [SerializeField]
    private GameObject juicePrefab;

    [SerializeField]
    private float juiceMakeTime = 2;

    [SerializeField]
    private ItemType fruitType;

    private CountdownHelper countdown;
    private bool isMachineWorking = false;

    private void Awake()
    {
        juiceMakeTime = PlayerPrefs.HasKey(nameof(juiceMakeTime)) ? PlayerPrefs.GetFloat(nameof(juiceMakeTime)) : juiceMakeTime;
        machineCapacity = PlayerPrefs.HasKey(nameof(machineCapacity)) ? PlayerPrefs.GetInt(nameof(machineCapacity)) : machineCapacity;
    }

    private void Start()
    {
        inputStack.Setup();
        outputStack.Setup();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(nameof(juiceMakeTime), juiceMakeTime);
        PlayerPrefs.SetInt(nameof(machineCapacity), machineCapacity);
    }

    private IEnumerator MakeJuice()
    {
        isMachineWorking = true;
        yield return new WaitForSeconds(juiceMakeTime);

        var fruit = inputStack.Remove();
        fruit.SetTarget(machineTransform, .2f, () => SimplePool.Despawn(fruit.gameObject));

        var juice = SimplePool.Spawn(juicePrefab, machineTransform.position, Quaternion.identity);
        outputStack.Add(juice.GetComponent<Item>(), null, false, 0);

        if (inputStack.ItemCount > 0)
        {
            StartCoroutine(MakeJuice());
        }
        else
        {
            isMachineWorking = false;
        }
    }

    public void AddToFactory(StackController stack)
    {
        if (inputStack.MaxCapacity <= inputStack.ItemCount) return;

        if (!isMachineWorking && stack.Contains(fruitType))
        {
            StartCoroutine(MakeJuice());
        }

        while (stack.Contains(fruitType))
        {
            var fruit = stack.Remove(fruitType);
            inputStack.Add(fruit);
            if (!SfxController.Instance.AllSoundsClose)
                SfxController.Instance.GetCollectSound();
        }
    }

    public void GiveJuice(IStacker stacker)
    {
        if (outputStack.ItemCount <= 0) return;
        if (stacker.Stack.ItemCount >= stacker.Capacity) return;

        var juice = outputStack.Remove();
        stacker.Stack.Add(juice, null, true);
        if (!SfxController.Instance.AllSoundsClose)
            SfxController.Instance.GetCollectSound();
    }

    public void UpgradeCapacity(int value)
    {
        machineCapacity += 2;
        Save();
    }

    public void UpgradeSpeed(int value)
    {
        juiceMakeTime -= .2f;
        Save();
    }
}
