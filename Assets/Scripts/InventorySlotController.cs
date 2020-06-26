using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour, IInventorySlot
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text itemCount;
    [SerializeField]
    private Image image;
    [SerializeField]
    private int slot;

    public Item Item { get; set; }
    public int Count { get; set; }

    public void Start()
    {
        GameEvents.instance.OnInventoryItemAdded += AddItem;
        GameEvents.instance.OnInventoryItemRemoved += RemoveItem;
    }

    public void AddItem(int slot, Item item)
    {
        if (this.slot == slot)
        {
            if (Item?.Id != item.Id)
            {
                title.text = item.Name;
                image.sprite = Resources.Load<Sprite>(item.Sprite);
                image.color = new Color(255, 255, 255, 255);
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
            if (Count == 1)
            {
                title.text = null;
                image.sprite = null;
                image.color = new Color(255, 255, 255, 0);
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
}
