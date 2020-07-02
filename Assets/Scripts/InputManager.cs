using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup menu;
    public bool menuIsShowing;
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
            if (menuIsShowing)
            {
                SetDefaultCursorState();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }

            if (menuIsShowing)
            {
                menu.alpha = 0f;
                menu.blocksRaycasts = false;
            }
            else
            {
                menu.alpha = 1f;
                menu.blocksRaycasts = true;
            }

            menuIsShowing = !menuIsShowing;
        }
    }
}