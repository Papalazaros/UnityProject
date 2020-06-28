using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public GameObject trackingObject;
    public RectTransform targetCanvas;
    private RectTransform rectTransform;
    private IHealth health;

    private void Start()
    {
        health = trackingObject.transform.GetComponent<IHealth>();
        rectTransform = GetComponent<RectTransform>();
        healthBar.gameObject.SetActive(true);
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
