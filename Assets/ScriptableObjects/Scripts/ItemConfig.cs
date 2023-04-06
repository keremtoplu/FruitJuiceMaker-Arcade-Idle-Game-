using Shadout.Models;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Config/ItemConfig")]
public class ItemConfig : ScriptableObject
{
    public ItemType ItemType;
    public int Price;
}