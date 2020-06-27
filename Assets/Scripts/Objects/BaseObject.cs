using UnityEngine;

public class BaseObject : MonoBehaviour, IGazeReceiver
{
    public int Id;
    protected bool isGazingUpon;
    protected bool textCreated;
    protected Camera mainCamera;
    protected GameObject text;
    protected TextMesh textMesh;
    protected Item item;

    protected void Awake()
    {
        item = ItemDatabase.Get(Id);
    }

    protected void Start()
    {
        mainCamera = Camera.main;
    }

    protected void Update()
    {
        if (isGazingUpon && !textCreated)
        {
            text = new GameObject($"{gameObject.name}Label");
            text.transform.SetParent(gameObject.transform);
            text.transform.position = GetTextPosition();
            text.transform.rotation = mainCamera.transform.rotation;
            textMesh = text.AddComponent<TextMesh>();
            textMesh.text = item?.Name ?? gameObject.transform.name;
            textMesh.characterSize = .01f;
            textMesh.fontSize = 100;
            textMesh.alignment = TextAlignment.Center;
            textMesh.anchor = TextAnchor.MiddleCenter;
            textCreated = true;
        }
        else if (isGazingUpon && textCreated)
        {
            text.transform.rotation = mainCamera.transform.rotation;
        }
    }

    private Vector3 GetTextPosition()
    {
        return gameObject.transform.position + (mainCamera.transform.rotation * (Vector3.up * 0.25f));
    }

    public void GazingUpon()
    {
        isGazingUpon = true;
    }

    public void NotGazingUpon()
    {
        Destroy(text);
        isGazingUpon = false;
        textCreated = false;
    }
}
