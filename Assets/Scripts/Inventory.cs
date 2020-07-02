using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField]
    private GameObject inventoryPanel;
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

    public bool Add(Item item, Guid itemInstanceId)
    {
        int? openSlot = GetOpenSlot(item);

        if (openSlot.HasValue)
        {
            GameEvents.instance.InventoryItemAdded(openSlot.Value, item, itemInstanceId);
        }

        return openSlot.HasValue;
    }
}
