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

    public event Action<int, Item, Guid> OnInventoryItemAdded;
    public event Action<Item, Guid> OnItemEquipped;
    public event Action<float> OnTimeOfDayChanged;
    public event Action<float> OnItemSelectedToCombine;

    public void InventoryItemAdded(int slot, Item item, Guid itemInstanceId)
    {
        OnInventoryItemAdded?.Invoke(slot, item, itemInstanceId);
    }

    public void ItemEquipped(Item item, Guid itemInstanceId)
    {
        OnItemEquipped?.Invoke(item, itemInstanceId);
    }

    public void TimeOfDayChanged(float timeOfDay)
    {
        OnTimeOfDayChanged?.Invoke(timeOfDay);
    }
}
