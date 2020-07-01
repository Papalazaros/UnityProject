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
    public event Action<Item> OnItemEquipped;
    public event Action<float> OnTimeOfDayChanged;

    public void InventoryItemAdded(int slot, Item item)
    {
        OnInventoryItemAdded?.Invoke(slot, item);
    }

    public void ItemEquipped(Item item)
    {
        OnItemEquipped?.Invoke(item);
    }

    public void TimeOfDayChanged(float timeOfDay)
    {
        OnTimeOfDayChanged?.Invoke(timeOfDay);
    }
}
