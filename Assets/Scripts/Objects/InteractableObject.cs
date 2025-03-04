﻿using UnityEngine;

public class InteractableObject : BaseObject
{
    public KeyCode key = KeyCode.E;

    protected bool ObjectActionTriggered()
    {
        return Input.GetKeyDown(key) && isGazingUpon;
    }

    protected new void Update()
    {
        if (ObjectActionTriggered() && Item is Consumable && Inventory.instance.Add(Item, ItemInstanceId))
        {
            Destroy(gameObject);
        }

        base.Update();
    }
}
