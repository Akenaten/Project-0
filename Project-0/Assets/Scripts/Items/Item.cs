using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;


[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public int itemId;
    public bool stackable;
    public int  amount;
    public int maxStackSize;
    public string itemName;
    public string itemDescription;
    public Sprite sprite;
    public SlotTag itemTag;
    public SlotEquipmentTag itemEquipmentTag;

    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;
}