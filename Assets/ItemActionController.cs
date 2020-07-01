using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemActionController : MonoBehaviour, IPointerClickHandler
{
    public ItemAction ActionText;
    public Text Text;

    public void Initialize()
    {
        Text.text = ActionText.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SendMessageUpwards("ItemActionClicked", ActionText);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            SendMessageUpwards("ItemActionCancelled");
        }
    }
}
