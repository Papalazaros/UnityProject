using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup InventoryPanel;
    [SerializeField]
    private CanvasGroup EquipmentPanel;
    public bool inputDisabled;
    public static InputManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        SetDefaultCursorState();
    }

    private void SetDefaultCursorState()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inputDisabled)
            {
                SetDefaultCursorState();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }

            if (inputDisabled)
            {
                InventoryPanel.alpha = 0f;
                EquipmentPanel.alpha = 0f;
                InventoryPanel.blocksRaycasts = false;
                EquipmentPanel.blocksRaycasts = false;
            }
            else
            {
                InventoryPanel.alpha = 1f;
                EquipmentPanel.alpha = 1f;
                InventoryPanel.blocksRaycasts = true;
                EquipmentPanel.blocksRaycasts = true;
            }

            inputDisabled = !inputDisabled;
        }
    }
}