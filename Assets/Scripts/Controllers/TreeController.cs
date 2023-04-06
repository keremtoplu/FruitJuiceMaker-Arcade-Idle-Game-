using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField]
    private List<Transform> emptyFruitPositions;

    [SerializeField]
    private GameObject fruitPrefab;

    [SerializeField]
    private float growUpTime = 5;

    private List<FruitController> collectables = new List<FruitController>();
    private List<Transform> occupiedPositions = new List<Transform>();

    public float GrowUpTime => growUpTime;
    public int CollectablesCount => collectables.Count;
    private int fruitCount = 0;

    private void Start()
    {
        fruitCount = emptyFruitPositions.Count;
        StartCoroutine(SpawnFruitNumerator());
    }

    private void OnTriggerStay(Collider other)
    {
        var stacker = other.GetComponent<IStacker>();
        if (stacker != null)
        {
            stacker.CollectFruit(this);

        }
    }

    private void SpawnFruit()
    {
        if (emptyFruitPositions.Count > 0)
        {
            var fruit = SimplePool.Spawn(fruitPrefab, emptyFruitPositions[0].position, Quaternion.identity);
            fruit.transform.SetParent(emptyFruitPositions[0]);
            fruit.SetActive(true);
            fruit.transform.localScale = fruit.transform.localScale * .01f;
            fruit.GetComponent<FruitController>().Setup(this);
        }

    }

    public void TakePosition(FruitController fruit)
    {
        emptyFruitPositions.Remove(fruit.transform.parent);
        occupiedPositions.Add(fruit.transform.parent);
    }

    public void AddCollectables(FruitController fruit)
    {
        collectables.Add(fruit);
    }

    public bool TryCollectFruit(out FruitController fruit)
    {
        if (collectables.Count > 0)
        {
            fruit = Remove();
            if (!SfxController.Instance.AllSoundsClose)
                SfxController.Instance.GetCollectSound();
            return true;
        }
        else
        {
            fruit = null;
            return false;
        }

    }

    private FruitController Remove()
    {
        var fruit = collectables[0];

        occupiedPositions.Remove(fruit.transform.parent);
        emptyFruitPositions.Add(fruit.transform.parent);

        collectables.RemoveAt(0);
        return fruit;
    }

    private IEnumerator SpawnFruitNumerator()
    {
        if (occupiedPositions.Count < fruitCount)
        {
            SpawnFruit();
        }

        yield return new WaitForSeconds(growUpTime);
        StartCoroutine(SpawnFruitNumerator());
    }
}
