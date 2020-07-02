using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateController : MonoBehaviour
{
    public static ObjectStateController instance;

    private Dictionary<Guid, Dictionary<string, object>> SavedItemStates;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SavedItemStates = new Dictionary<Guid, Dictionary<string, object>>();
        }
    }

    public void Set(Guid id, Dictionary<string, object> state)
    {
        instance.SavedItemStates[id] = state;
    }

    public Dictionary<string, object> Get(Guid id)
    {
        instance.SavedItemStates.TryGetValue(id, out Dictionary<string, object> state);
        Delete(id);
        return state;
    }

    public void Delete(Guid id)
    {
        instance.SavedItemStates.Remove(id);
    }
}
