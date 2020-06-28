using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 currentRotation;

    private void Update()
    {
        currentRotation.y -= Input.GetAxis("Mouse Y");
        currentRotation.y = Mathf.Clamp(currentRotation.y, -90f, 90f);
    }

    private void LateUpdate()
    {
        transform.position = Player.instance.originPoint.transform.position;
        transform.eulerAngles = new Vector3(currentRotation.y, Player.instance.transform.eulerAngles.y, 0);
    }
}
