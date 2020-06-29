using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    public Slider healthBar;
    private GameObject trackingObject;
    private RectTransform targetCanvas;
    private RectTransform rectTransform;
    private IHealth health;
    private bool isActivated;

    private void Start()
    {
        trackingObject = transform.parent.gameObject;
        health = trackingObject.transform.GetComponent<IHealth>();
        Canvas canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        transform.SetParent(canvas.transform);
        targetCanvas = canvas.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (trackingObject == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(trackingObject.transform.position);
            rectTransform.anchoredPosition = new Vector2(
            (ViewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * 0.5f),
            (ViewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * 0.5f));
            healthBar.value = health.CurrentHealth;
        }
    }
}
