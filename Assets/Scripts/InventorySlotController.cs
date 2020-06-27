﻿using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour, IInventorySlot
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text itemCount;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private int slot;
    private Image slotImage;
    private Color initialColor;

    public Item Item { get; set; }
    public int Count { get; set; }

    public void Start()
    {
        slotImage = GetComponent<Image>();
        initialColor = slotImage.color;
        GameEvents.instance.OnInventoryItemAdded += AddItem;
        GameEvents.instance.OnInventoryItemUsed += UseItem;
        GameEvents.instance.OnInventoryItemDropped += RemoveItem;
        GameEvents.instance.OnInventorySlotSelected += InventorySlotSelected;
    }

    public void InventorySlotSelected(int slot)
    {
        if (this.slot == slot)
        {
            slotImage.color = new Color(200, 200, 200, 175);
        }
        else
        {
            slotImage.color = initialColor;
        }
    }

    public void AddItem(int slot, Item item)
    {
        if (this.slot == slot)
        {
            if (Item?.Id != item.Id)
            {
                title.text = item.Name;
                itemImage.sprite = Resources.Load<Sprite>(item.Sprite);
                itemImage.color = new Color(255, 255, 255, 255);
                Item = item;
            }

            Count++;
            itemCount.text = Count.ToString();
        }
    }

    public void RemoveItem(int slot, Item item)
    {
        if (this.slot == slot)
        {
            // Destroy equipped item on removal
            if (Player.instance.equippedItemObject != null && Player.instance.equippedItemObject.Id == item.Id)
            {
                Destroy(Player.instance.equippedItemObject.gameObject);
            }

            if (Count == 1)
            {
                title.text = null;
                itemImage.sprite = null;
                itemImage.color = new Color(255, 255, 255, 0);
                itemImage.preserveAspect = true;
                Item = null;
                itemCount.text = null;
                Count--;
            }
            else
            {
                Count--;
                itemCount.text = Count.ToString();
            }
        }
    }

    public void UseItem(int slot, Item item)
    {
        if (this.slot == slot)
        {
            if (item is Equippable equippable)
            {
                GameEvents.instance.ItemEquipped(equippable);
            }
            else
            {
                RemoveItem(slot, item);
            }
        }
    }
}
