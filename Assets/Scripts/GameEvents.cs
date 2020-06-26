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
        else
        {
            Destroy(gameObject);
        }
    }

    public event Action<int, Item> OnInventoryItemAdded;
    public event Action<int, Item> OnInventoryItemRemoved;

    public void InventoryItemAdded(int slot, Item item)
    {
        OnInventoryItemAdded?.Invoke(slot, item);
    }

    public void InventoryItemRemoved(int slot, Item item)
    {
        OnInventoryItemRemoved?.Invoke(slot, item);
    }
}
