using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public event Action<int, Item> OnInventoryItemAdded;
    public event Action<int, Item> OnInventoryItemUsed;
    public event Action<int> OnInventorySlotSelected;
    public event Action<Equippable> OnItemEquipped;

    public void InventoryItemAdded(int slot, Item item)
    {
        OnInventoryItemAdded?.Invoke(slot, item);
    }

    public void InventoryItemUsed(int slot, Item item)
    {
        OnInventoryItemUsed?.Invoke(slot, item);
    }

    public void InventorySlotSelected(int slot)
    {
        OnInventorySlotSelected?.Invoke(slot);
    }

    public void ItemEquipped(Equippable item)
    {
        OnItemEquipped?.Invoke(item);
    }
}
