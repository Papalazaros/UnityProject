using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    public Text text;
    public Image image;
    public int slot;
    public Item currentItem;

    private void LateUpdate()
    {
        Item item = Inventory.instance.GetItemAtPosition(slot);
        if (currentItem != null && item == null)
        {
            text.text = null;
            image.sprite = null;
            image.color = new Color(255, 255, 255, 0);
            currentItem = null;
        }
        if (item != null)
        {
            if (currentItem?.Id == item.Id) return;
            text.text = item.Name;
            image.sprite = Resources.Load<Sprite>(item.Sprite);
            image.color = new Color(255, 255, 255, 255);
            currentItem = item;
        }
        else if (currentItem != null)
        {
            currentItem = null;
        }
    }
}
