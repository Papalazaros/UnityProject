using UnityEngine;
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

    private bool detailsPanelExpanded;
    public GameObject detailsPanel;
    private ItemDetailsController itemDetailsController;

    public Item Item { get; set; }
    public int Count { get; set; }

    private void Awake()
    {
        slotImage = GetComponent<Image>();
        itemDetailsController = detailsPanel.GetComponent<ItemDetailsController>();
        initialColor = slotImage.color;
    }

    public void OnClicked()
    {
        if (Item?.Actions != null)
        {
            detailsPanelExpanded = !detailsPanelExpanded;
            detailsPanel.SetActive(detailsPanelExpanded);

            if (!itemDetailsController.isInitialized)
            {
                itemDetailsController.CreateActions(Item.Actions);
                itemDetailsController.Slot = slot;
            }
        }
    }

    private void DisposeItemDetails()
    {
        itemDetailsController.ResetActions();
        detailsPanelExpanded = false;
        detailsPanel.SetActive(false);
    }

    public void Start()
    {
        GameEvents.instance.OnInventoryItemAdded += Add;
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

    public void Add(int slot, Item item)
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

    public void Remove()
    {
        // Destroy equipped item on removal
        if (Player.instance.equippedItemObject != null && Player.instance.equippedItemObject.Id == Item.Id)
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
            DisposeItemDetails();
        }
        else
        {
            Count--;
            itemCount.text = Count.ToString();
        }
    }

    public void Drop()
    {
        GameObject droppedItem = Instantiate(Resources.Load<GameObject>(Item.Prefab));
        droppedItem.transform.position = Player.instance.transform.position + (Player.instance.transform.rotation * Vector3.forward);
        Remove();
    }

    public void ItemActionCancelled()
    {
        detailsPanelExpanded = false;
        detailsPanel.SetActive(false);
    }

    public void ItemActionClicked(ItemAction itemAction)
    {
        switch (itemAction)
        {
            case ItemAction.Drop:
                Drop();
                break;
            case ItemAction.Use:
                if (Item.Use()) Remove();
                break;
            case ItemAction.Equip:
                GameEvents.instance.ItemEquipped(Item);
                break;
        }
    }
}
