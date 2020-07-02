using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    public Slider healthBar;
    private GameObject trackingObject;
    private RectTransform targetCanvas;
    private RectTransform rectTransform;
    private IHealth health;
    private Camera mainCamera;
    private bool isReady;

    public void UpdatePosition()
    {
        var offset = trackingObject.GetComponent<MeshFilter>().mesh.bounds.extents;
        offset.x = 0;
        offset.z = 0;
        Vector2 ViewportPosition = mainCamera.WorldToViewportPoint(trackingObject.transform.position + offset);
        rectTransform.anchoredPosition = new Vector2(
        (ViewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * 0.5f),
        (ViewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * 0.5f));
    }

    public void UpdatePosition(GameObject trackingObject, Camera mainCamera)
    {
        this.mainCamera = mainCamera;
        this.trackingObject = trackingObject;
        gameObject.transform.SetParent(trackingObject.transform);
        health = trackingObject.GetComponent<IHealth>();
        Canvas canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        transform.SetParent(canvas.transform);
        targetCanvas = canvas.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        transform.rotation = new Quaternion();
        UpdatePosition();
        isReady = true;
    }

    private void Update()
    {
        if (trackingObject == null)
        {
            Destroy(gameObject);
        }
        else if (trackingObject != null && isReady)
        {
            UpdatePosition();
            healthBar.value = health.CurrentHealth;
        }
    }
}
