using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour//, IPointerClickHandler
{
    Image itemIcon;
    // public CanvasGroup canvasGroup { get; private set; }

    public Item myItem { get; set; }
    public int amount;
    public InventorySlot activeSlot { get; set; }

    void Awake()
    {
        // canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        activeSlot = parent;
        activeSlot.myItem = this;
        activeSlot.itemImage.sprite = item.sprite;
        activeSlot.itemName.text = item.itemName;
        if (item.stackable)
        {
            AddToStack(item.amount);
        }
        myItem = item;
    }

    public void AddToStack(int addAmount)
    {
        amount += addAmount;
        activeSlot.itemStackNumbertext.text = amount.ToString();
    }

    // public void OnPointerClick(PointerEventData eventData)
    // {
    //     if(eventData.button == PointerEventData.InputButton.Left)
    //     {
    //         Inventory.Singleton.SetSelectedItem(this);
    //     }
    // }
}