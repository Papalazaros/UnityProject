using UnityEngine;

public class GazeManager : MonoBehaviour
{
    [SerializeField]
    private float gazeDistance;
    private Camera mainCamera;
    private GameObject lastGazedUpon;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        CheckGaze();
    }

    private void CheckGaze()
    {
        Ray gazeRay = new Ray(mainCamera.transform.position, mainCamera.transform.rotation * Vector3.forward);

        if (Physics.Raycast(gazeRay, out RaycastHit hit, gazeDistance))
        {
            if (!lastGazedUpon || lastGazedUpon != hit.transform.gameObject) hit.transform.SendMessage("GazingUpon", SendMessageOptions.DontRequireReceiver);
            if (lastGazedUpon && lastGazedUpon != hit.transform.gameObject) lastGazedUpon.SendMessage("NotGazingUpon", SendMessageOptions.DontRequireReceiver);

            lastGazedUpon = hit.transform.gameObject;
        }
        else if (lastGazedUpon)
        {
            lastGazedUpon.SendMessage("NotGazingUpon", SendMessageOptions.DontRequireReceiver);
            lastGazedUpon = null;
        }
    }
}