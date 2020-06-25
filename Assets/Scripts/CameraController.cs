using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, 1, -1.5f);
    private Vector3 currentRotation;

    private void Start()
    {
    }

    //private void OnGUI()
    //{
    //    float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
    //    float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
    //    GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
    //}

    private void Update()
    {
        currentRotation.y -= Input.GetAxis("Mouse Y");
        currentRotation.y = Mathf.Clamp(currentRotation.y, -90f, 90f);
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position;
        transform.eulerAngles = new Vector3(currentRotation.y, player.transform.eulerAngles.y, 0);
    }
}
