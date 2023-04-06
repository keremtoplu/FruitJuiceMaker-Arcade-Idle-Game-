using System;
using System.Collections.Generic;
using Shadout.Models;
using UnityEngine;

namespace Shadout.Controllers
{
    public class StackController : MonoBehaviour
    {
        #region SerializedFields

        [SerializeField]
        private int xLen, yLen, zLen;

        [SerializeField]
        private Vector3 stackSizeOffset;

        [Header("Players Stack Settings")]
        [SerializeField]
        private float moveDistancePerItem = .1f;
        [SerializeField]
        private float itemMoveSpeed = .005f;

        [SerializeField]
        private float itemReturnSpeed = .003f;

        #endregion

        #region Variables

        private bool moveValueHandler = false;
        private Transform[] itemPositions;
        private List<Item> items = new List<Item>();
        private Dictionary<ItemType, int> itemDict = new Dictionary<ItemType, int>();
        private bool isMoving = false;

        private const int MaxYVal = 16;


        #endregion

        #region Events


        public event Action ItemCountUpdated;

        public event Action ItemAdded;

        public event Action ItemRemoved;


        #endregion

        #region Props

        public int MaxCapacity => xLen * yLen * zLen;

        public int ItemCount { get; private set; }

        public bool IsFull
        {
            get
            {
                // if (capacityUpgradable == null)
                return transform.parent.GetComponent<IStacker>().Capacity == ItemCount;
                // else
                //     return ItemCount == capacityUpgradable.Capacity;
            }
        }

        public bool IsEmpty => ItemCount == 0;


        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        public void Setup()
        {
            //this.capacityUpgradable = capacityUpgradable;

            // if (capacityUpgradable != null)
            // {
            //     capacityUpgradable.Upgraded += OnCapacityUpgraded;
            // }
            GenerateItemPositions();
        }

        private void GenerateItemPositions()
        {
            if (stackSizeOffset.x == 0f || stackSizeOffset.y == 0f || stackSizeOffset.z == 0f)
            {
                Debug.LogError("Stack offset cannot be 0");
                return;
            }

            itemPositions = new Transform[MaxCapacity];
            int itemCounter = 0;

            for (int i = 0; i < yLen; i++)
            {
                for (int j = 0; j < zLen; j++)
                {
                    for (int k = 0; k < xLen; k++)
                    {
                        var pointObject = new GameObject($"position {k} {j} {i}");
                        var pointTransform = pointObject.transform;
                        pointObject.transform.SetParent(transform);

                        pointTransform.localPosition = new Vector3(k * stackSizeOffset.x, i * stackSizeOffset.y, j * stackSizeOffset.z);
                        itemPositions[itemCounter++] = pointTransform;
                    }
                }
            }
        }

        private void RearrangePositions()
        {
            for (int i = 0; i < ItemCount; i++)
            {
                int index = i;
                items[i].transform.SetParent(itemPositions[i]);
                items[i].SetTarget(itemPositions[i], .1f, () => { items[index].transform.localScale = Item.ItemScale; }, false);
            }
        }

        public void Add(Item item, Action onAddCompleted = null, bool isStackHolder = false, float animTime = .3f)
        {
            // if (capacityUpgradable != null)
            // {
            //     if (ItemCount >= capacityUpgradable.Capacity)
            //         return;
            // }

            ItemCount++;

            var itemIndex = ItemCount - 1;
            item.SetTarget(
                itemPositions[ItemCount - 1],
                animTime,//AnimatorSettings.Instance.StorageCollectAnimTime,
                () =>
                {
                    items.Add(item);
                    item.transform.SetParent(itemPositions[itemIndex]);
                    item.transform.localScale = Item.ItemScale;

                    if (isMoving)
                    {
                        LeanTween.cancel(items[itemIndex].gameObject);
                        items[itemIndex].transform.position = itemPositions[itemIndex].position + itemPositions[itemIndex].forward * -moveDistancePerItem * (itemIndex % yLen);
                    }

                    onAddCompleted?.Invoke();
                }
            ).WithRotation(isStackHolder);

            int typeCount;
            if (itemDict.TryGetValue(item.ItemType, out typeCount))
            {
                itemDict[item.ItemType] = typeCount + 1;
            }
            else
            {
                itemDict.Add(item.ItemType, 1);
            }

            ItemCountUpdated?.Invoke();
            ItemAdded?.Invoke();
        }

        public Item Remove()
        {
            return Remove(items[items.Count - 1].ItemType);
        }

        public Item Remove(ItemType itemType, bool isStackHolder = false)
        {
            Item item = null;
            int typeCount;

            // if(itemType == ItemType.Any)
            // {
            //     var index = ItemCount - 1;
            //     item = items[index];
            //     itemDict[item.ItemType]  -= 1;
            //     items.RemoveAt(index);
            //     if(itemDict[item.ItemType] <= 0)
            //     {
            //         itemDict.Remove(item.ItemType);
            //     }
            //     ItemCount --;
            //     return item;
            // }

            if (itemDict.TryGetValue(itemType, out typeCount))
            {
                itemDict[itemType] = typeCount - 1;
                int itemIndex = -1;
                for (int i = ItemCount - 1; i >= 0; --i)
                {
                    if (items[i].ItemType == itemType)
                    {
                        item = items[i];
                        itemIndex = i;
                        break;
                    }
                }

                if (typeCount - 1 <= 0)
                    itemDict.Remove(itemType);

                items.RemoveAt(itemIndex);
                ItemCount--;
            }
            else
                return null;

            RearrangePositions();
            ItemCountUpdated?.Invoke();

            ItemRemoved?.Invoke();

            return item;
        }

        public bool Contains(ItemType itemType)
        {
            return itemDict.ContainsKey(itemType);
        }

        public void MoveStack(bool isMoving)
        {
            this.isMoving = isMoving;
            if (moveValueHandler == isMoving) return;

            if (isMoving)
            {
                moveValueHandler = isMoving;

                for (int i = 0; i < items.Count; i++)
                {
                    int value = i;
                    LeanTween.cancel(items[value].gameObject);
                    items[value].transform.position = itemPositions[value].position;
                    items[value].transform.LeanMoveLocalZ(-(value % MaxYVal) * moveDistancePerItem, (value % MaxYVal) * itemMoveSpeed);
                }
            }
            else
            {
                moveValueHandler = isMoving;

                for (int i = 0; i < items.Count; i++)
                {
                    int value = i;
                    LeanTween.cancel(items[value].gameObject);
                    items[value].transform.LeanMoveLocalZ(0, (value % MaxYVal) * itemReturnSpeed);
                }
            }
        }

        public void ClearStack()
        {
            foreach (var item in items)
            {
                SimplePool.Despawn(item.gameObject);
            }

            ItemCount = 0;
            isMoving = false;
            moveValueHandler = false;

            itemDict.Clear();
            items.Clear();
        }


        #endregion

        #region Callbacks

        #endregion
    }
}