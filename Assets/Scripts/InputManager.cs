using UnityEngine;

public class InputManager : MonoBehaviour
{
    public CanvasGroup menu;
    private bool isShowing;

    private void Awake()
    {
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
            if (isShowing)
            {
                SetDefaultCursorState();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }

            if (isShowing)
            {
                menu.alpha = 0f;
                menu.blocksRaycasts = false;
            }
            else
            {
                menu.alpha = 1f;
                menu.blocksRaycasts = true;
            }

            isShowing = !isShowing;
        }
    }
}