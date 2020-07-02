using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour, IGazeReceiver
{
    public int ItemId;

    private Guid _itemInstanceId;
    public Guid ItemInstanceId
    {
        get
        {
            return _itemInstanceId;
        }
        set
        {
            bool updateState = _itemInstanceId != Guid.Empty;
            _itemInstanceId = value;
            if (updateState) AssignObjectState();
        }
    }

    protected Dictionary<string, object> objectState;
    protected bool isGazingUpon;
    protected bool textCreated;
    protected Camera mainCamera;
    protected GameObject text;
    protected TextMesh textMesh;
    protected Item item;

    protected void Awake()
    {
        item = ItemDatabase.Get(ItemId);
        ItemInstanceId = Guid.NewGuid();
    }

    protected void Start()
    {
        mainCamera = Camera.main;
    }

    private void AssignObjectState()
    {
        objectState = ObjectStateController.instance.Get(ItemInstanceId);
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

    private void OnDestroy()
    {
        ObjectStateController.instance.Set(ItemInstanceId, objectState);
    }
}
