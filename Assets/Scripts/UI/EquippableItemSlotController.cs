using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EquippableItemSlotController : MonoBehaviour
{
    [SerializeField]
    private SlotType SlotType;
    [SerializeField]
    private Image Image;
    [SerializeField]
    private Text Text;

    private void Awake()
    {
        Text.text = SlotType.ToString();
    }

    private void Start()
    {
        GameEvents.instance.OnEquippedItemChanged += EquippedItemChanged;
    }

    private void EquippedItemChanged(SlotType slotType)
    {
        if (SlotType == slotType)
        {
            EquippableObject equippableObject = Player.instance.equippedItems[slotType];

            if (equippableObject == null)
            {
                Image.sprite = null;
                Image.color = new Color(255, 255, 255, 0);
            }
            else
            {
                Image.sprite = AssetLoader.instance.Get<Sprite>($"Sprites/{equippableObject.Item.Id}");
                Image.preserveAspect = true;
                Image.color = new Color(255, 255, 255, 255);
            }
        }
    }
}
