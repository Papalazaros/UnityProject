using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public GameObject inventoryPanel;
    private int? selectedSlot;

    private int totalCapacity;
    private int remainingCapacity;

    private Dictionary<int, Image> cachedInventoryImages;
    private IInventorySlot[] inventorySlots;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // Get available inventory slots from UI component
            totalCapacity = inventoryPanel.transform.childCount;
            remainingCapacity = inventoryPanel.transform.childCount;
            cachedInventoryImages = new Dictionary<int, Image>(totalCapacity);
            inventorySlots = inventoryPanel.GetComponentsInChildren<IInventorySlot>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private int? GetOpenSlot(Item item)
    {
        for (int i = 0; i < totalCapacity; i++)
        {
            IInventorySlot inventorySlot = inventorySlots[i];
            Item itemInSlot = inventorySlot.Item;

            if (itemInSlot == null || inventorySlot.Count + 1 <= item.MaxStackSize)
            {
                return i;
            }
        }

        return null;
    }

    public Image GetInventorySlotImage(int slot)
    {
        if (!cachedInventoryImages.TryGetValue(slot, out Image image))
        {
            image = inventoryPanel.transform.GetChild(slot).GetComponent<Image>();
            cachedInventoryImages.Add(slot, image);
        }

        return image;
    }

    private void Update()
    {
        int? currentSelectedSlot = GetSelectedItem();

        if (currentSelectedSlot.HasValue
            && selectedSlot != currentSelectedSlot)
        {
            if (selectedSlot.HasValue)
            {
                GetInventorySlotImage(selectedSlot.Value).color = new Color(200, 200, 200, 0);
            }

            GetInventorySlotImage(currentSelectedSlot.Value).color = new Color(200, 200, 200, 175);
            selectedSlot = currentSelectedSlot;
        }

        if (selectedSlot <= totalCapacity
            && Input.GetKeyDown(KeyCode.Return))
        {
            Item item = inventorySlots[selectedSlot.Value].Item;

            if (item != null)
            {
                GameEvents.instance.InventoryItemRemoved(selectedSlot.Value, item);
                item.Consume();
                remainingCapacity++;
            }
        }

        if (selectedSlot <= totalCapacity
            && Input.GetKeyDown(KeyCode.G))
        {
            Item item = inventorySlots[selectedSlot.Value].Item;

            if (item != null)
            {
                GameEvents.instance.InventoryItemRemoved(selectedSlot.Value, item);
                Drop(item);
                remainingCapacity++;
            }
        }
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

    public void Add(Item item)
    {
        if (remainingCapacity == 0) return;

        int? openSlot = GetOpenSlot(item);

        if (openSlot.HasValue)
        {
            remainingCapacity--;
            GameEvents.instance.InventoryItemAdded(openSlot.Value, item);
        }
    }

    public static void Drop(Item item)
    {
        GameObject droppedItem = Instantiate(Resources.Load<GameObject>(item.Prefab));
        Rigidbody rigidBody = droppedItem.GetComponent<Rigidbody>();
        if (rigidBody != null) rigidBody.isKinematic = false;
        droppedItem.transform.position = Player.instance.transform.position + (Player.instance.transform.rotation * Vector3.forward);
    }
}
