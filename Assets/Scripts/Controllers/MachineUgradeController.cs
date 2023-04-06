using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;

public class MachineUgradeController : MonoBehaviour
{
    [SerializeField]
    private List<FactoryController> appleJuiceMachines;
    [SerializeField]
    private List<FactoryController> orangeJuiceMachines;
    [SerializeField]
    private List<FactoryController> lemonadeMachines;
    [SerializeField]
    private GameObject machineUpgradeStore;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            UIController.Instance.OpenUpgradeMachinePanel();
        }
    }

    public void UpgradeAppleMachineCapacity(int value)
    {
        CurrencyController.Instance.Decrease(value);
        if (!SfxController.Instance.AllSoundsClose)
            SfxController.Instance.GetCashOutSound();
        for (int i = 0; i < appleJuiceMachines.Count; i++)
        {
            appleJuiceMachines[i].UpgradeCapacity(value);
        }
    }
    public void UpgradeAppleMachineSpeed(int value)
    {
        CurrencyController.Instance.Decrease(value);
        if (!SfxController.Instance.AllSoundsClose)
            SfxController.Instance.GetCashOutSound();
        for (int i = 0; i < appleJuiceMachines.Count; i++)
        {
            appleJuiceMachines[i].UpgradeSpeed(value);
        }
    }
    public void UpgradeOrangeMachineCapacity(int value)
    {
        CurrencyController.Instance.Decrease(value);
        if (!SfxController.Instance.AllSoundsClose)
            SfxController.Instance.GetCashOutSound();
        for (int i = 0; i < orangeJuiceMachines.Count; i++)
        {
            orangeJuiceMachines[i].UpgradeCapacity(value);
        }
    }
    public void UpgradeOrangeMachineSpeed(int value)
    {
        CurrencyController.Instance.Decrease(value);
        if (!SfxController.Instance.AllSoundsClose)
            SfxController.Instance.GetCashOutSound();
        for (int i = 0; i < orangeJuiceMachines.Count; i++)
        {
            orangeJuiceMachines[i].UpgradeSpeed(value);
        }
    }
    public void UpgradeLemonadeMachineCapacity(int value)
    {
        CurrencyController.Instance.Decrease(value);
        if (!SfxController.Instance.AllSoundsClose)
            SfxController.Instance.GetCashOutSound();
        for (int i = 0; i < lemonadeMachines.Count; i++)
        {
            lemonadeMachines[i].UpgradeCapacity(value);
        }
    }
    public void UpgradeLemonadeMachineSpeed(int value)
    {
        CurrencyController.Instance.Decrease(value);
        if (!SfxController.Instance.AllSoundsClose)
            SfxController.Instance.GetCashOutSound();

        for (int i = 0; i < lemonadeMachines.Count; i++)
        {
            lemonadeMachines[i].UpgradeSpeed(value);
        }
    }
    public void CloseUpgradeStore()
    {
        machineUpgradeStore.SetActive(false);
    }
}
