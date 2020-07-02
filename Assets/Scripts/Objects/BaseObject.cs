using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour, IGazeReceiver
{
    public int ItemId;
    public Item Item;

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

    protected bool isGazingUpon;
    protected bool textCreated;
    protected Camera mainCamera;
    protected GameObject text;
    protected TextMesh textMesh;

    protected void Awake()
    {
        Item = ItemDatabase.Get(ItemId);
        ItemInstanceId = Guid.NewGuid();
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
            textMesh.text = Item?.Name ?? gameObject.transform.name;
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

    public virtual Dictionary<string, object> GetObjectState()
    {
        return null;
    }

    public virtual void AssignObjectState()
    {
    }

    private void OnDestroy()
    {
        Debug.Log(ItemInstanceId);
        ObjectStateController.instance.Set(ItemInstanceId, GetObjectState());
    }
}
