﻿using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField]
    private GameObject inventoryPanel;
    private int? selectedSlot;
    private int totalSlots;
    private IInventorySlot[] inventorySlots;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // Get available inventory slots from UI component
            totalSlots = inventoryPanel.transform.childCount;
            inventorySlots = inventoryPanel.GetComponentsInChildren<IInventorySlot>();
        }
    }

    //private void Update()
    //{
    //    int? currentSelectedSlot = GetSelectedItem();

    //    if (currentSelectedSlot.HasValue
    //        && selectedSlot != currentSelectedSlot
    //        && currentSelectedSlot + 1 <= totalSlots)
    //    {
    //        selectedSlot = currentSelectedSlot;
    //        GameEvents.instance.InventorySlotSelected(selectedSlot.Value);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Return))
    //    {
    //        if (inventorySlots[selectedSlot.Value].Count == 0) return;

    //        Item item = inventorySlots[selectedSlot.Value].Item;

    //        if (item != null)
    //        {
    //            bool isUsed = item.Use();
    //            if (isUsed) GameEvents.instance.InventoryItemUsed(selectedSlot.Value, item);
    //        }
    //    }

    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        if (inventorySlots[selectedSlot.Value].Count == 0) return;

    //        Item item = inventorySlots[selectedSlot.Value].Item;

    //        if (item != null)
    //        {
    //            Drop(item);
    //        }
    //    }
    //}

    private int? GetOpenSlot(Item item)
    {
        for (int i = 0; i < totalSlots; i++)
        {
            IInventorySlot inventorySlot = inventorySlots[i];
            Item itemInSlot = inventorySlot.Item;

            if (itemInSlot == null || (item.Id == itemInSlot.Id && inventorySlot.Count + 1 <= item.MaxStackSize))
            {
                return i;
            }
        }

        return null;
    }

    private int? GetSelectedItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            return 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            return 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            return 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            return 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            return 8;
        }

        return null;
    }

    public bool Add(Item item)
    {
        int? openSlot = GetOpenSlot(item);

        if (openSlot.HasValue)
        {
            GameEvents.instance.InventoryItemAdded(openSlot.Value, item);
        }

        return openSlot.HasValue;
    }

    //public void Drop(Item item)
    //{
    //    GameObject droppedItem = Instantiate(Resources.Load<GameObject>(item.Prefab));
    //    droppedItem.transform.position = Player.instance.transform.position + (Player.instance.transform.rotation * Vector3.forward);
    //    GameEvents.instance.InventoryItemDropped(selectedSlot.Value, item);
    //}
}
