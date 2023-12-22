using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;
    public InventoryItem selectedItem;
    private InventoryItem tmpItem;
    public InventorySlot selectedInventorySlotItem;
    private InventorySlot tmpInventorySlotItem;

    [SerializeField] InventorySlot[] ogreInventorySlots;
    [SerializeField] InventorySlot[] goblinInventorySlots;
    [SerializeField] InventorySlot[] otherInventorySlots;
    [SerializeField] InventorySlot[] hotbarSlots;

    [SerializeField] InventorySlot[] ogreEquipmentSlots;
    [SerializeField] InventorySlot[] goblinEquipmentSlots;

    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] Item[] items;

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    void Awake()
    {
        Singleton = this;
        giveItemBtn.onClick.AddListener( delegate { SpawnInventoryItem(); } );
        ResetSelectedItem();
    }

    void Update()
    {
    }

    public void ResetSelectedItem()
    {
        selectedItem = null;
        if (selectedInventorySlotItem != null)
        {
            selectedInventorySlotItem.ResetSlot();
            selectedInventorySlotItem = null;
        } 
    }

    public void SwapSlot(InventorySlot slot)
    {
        tmpItem = slot.myItem;
        tmpInventorySlotItem = selectedInventorySlotItem;
        slot.SetItem(selectedItem);
        tmpInventorySlotItem.SetItem(tmpItem);

    }

    public void SetSelectedItem(InventoryItem item, InventorySlot itemSlot)
    {
        ResetSelectedItem();

       selectedItem = item;
       selectedInventorySlotItem = itemSlot;
        // if (item.activeSlot.slotEquipementTag != SlotEquipmentTag.None && item.activeSlot.myTag != selectedItem.myItem.itemTag)
        // {
        //     return;
        // }
        // item.activeSlot.SetItem(selectedItem);
    }

    // public void SetCarriedItem(InventoryItem item)
    // {
    //     if(carriedItem != null)
    //     {
    //         if(item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
    //         item.activeSlot.SetItem(carriedItem);
    //     }

    //     if(item.activeSlot.myTag != SlotTag.None)
    //     { EquipEquipment(item.activeSlot.myTag, null); }

    //     carriedItem = item;
    //     carriedItem.canvasGroup.blocksRaycasts = false;
    //     item.transform.SetParent(draggablesTransform);
    // }

    public void EquipEquipment(SlotTag tag, InventoryItem item = null)
    {
        switch (tag)
        {
            case SlotTag.Ogre:
                break;
            case SlotTag.Goblin:
                break;
        }
    }

    public void SpawnInventoryItem(Item item = null)
    {
        Item _item = item;
        if(_item == null)
        { _item = PickRandomItem(); }
        switch (_item.itemTag)
        {
            case SlotTag.Ogre:
                
                for (int i = 0; i < ogreInventorySlots.Length; i++)
                {
                    // Check if the slot is empty
                    if(ogreInventorySlots[i].myItem == null)
                    {
                        Instantiate(itemPrefab, ogreInventorySlots[i].transform).Initialize(_item, ogreInventorySlots[i]);
                        break;
                    }
                }
                break;
            case SlotTag.Goblin:
                for (int i = 0; i < goblinInventorySlots.Length; i++)
                {
                    // Check if the slot is empty
                    if(goblinInventorySlots[i].myItem == null)
                    {
                        Instantiate(itemPrefab, goblinInventorySlots[i].transform).Initialize(_item, goblinInventorySlots[i]);
                        break;
                    }
                }
                break;
            case SlotTag.None:
                if (_item.stackable)
                {
                    for (int i = 0; i < otherInventorySlots.Length; i++)
                    {
                        // Check if we already have stackable item
                        if(otherInventorySlots[i].myItem != null && otherInventorySlots[i].myItem.myItem.itemId == _item.itemId)
                        {
                            otherInventorySlots[i].myItem.AddToStack(_item.amount);
                            return ;
                        }
                    }
                }
                for (int i = 0; i < otherInventorySlots.Length; i++)
                {
                    // Check if the slot is empty
                    if(otherInventorySlots[i].myItem == null)
                    {
                        Instantiate(itemPrefab, otherInventorySlots[i].transform).Initialize(_item, otherInventorySlots[i]);
                        break;
                    }
                }
                break;
        }
    }

    Item PickRandomItem()
    {
        int random = Random.Range(0, items.Length);
        return items[random];
    }
}