using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System;

public enum SlotTag { None, Ogre, Goblin }
public enum SlotEquipmentTag { None, Head, Arms, Legs, Weapon, Cape, Eye}

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public InventoryItem myItem;

    public SlotTag myTag;
    public SlotEquipmentTag slotEquipementTag;

    public TMP_Text itemName;
    public TMP_Text itemStackNumbertext;
    public Image itemImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.Singleton.selectedItem != null
            && (slotEquipementTag == SlotEquipmentTag.None || slotEquipementTag == Inventory.Singleton.selectedItem.myItem.itemEquipmentTag))
            {
                if (myItem != null)
                {
                    Inventory.Singleton.SwapSlot(this);
                } else
                {
                    SetItem(Inventory.Singleton.selectedItem);
                }
            } else if (Inventory.Singleton.selectedItem == null && myItem != null)
            {
                Inventory.Singleton.SetSelectedItem(myItem, this);
            }
        }
    }

    public void SetItem(InventoryItem item)
    {
        // Set current slot
        myItem = item;
        myItem.activeSlot = this;

        item.Initialize(item.myItem, this);
        itemStackNumbertext.text = item.amount.ToString();
        myItem.transform.SetParent(transform);
        Inventory.Singleton.ResetSelectedItem();
        // myItem.canvasGroup.blocksRaycasts = true;

        if(myTag != SlotTag.None)
        { Inventory.Singleton.EquipEquipment(myTag, myItem); }
    }

    public void ResetSlot()
    {
        myItem = null;
        itemImage.sprite = null;
        itemName.text = "";
        itemStackNumbertext.text = "";
    }
}