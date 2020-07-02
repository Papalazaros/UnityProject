using System;
using System.Collections.Generic;
using System.Linq;
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
        if (state == null) return;
        instance.SavedItemStates[id] = state;
    }

    public Dictionary<string, object> Get(Guid id)
    {
        instance.SavedItemStates.TryGetValue(id, out Dictionary<string, object> state);
        LogState(state);
        return state;
    }

    public void Delete(Guid id)
    {
        instance.SavedItemStates.Remove(id);
    }

    private void LogState(Dictionary<string, object> state)
    {
        if (state == null) return;
        Debug.Log(string.Join("\n", state?.Select(x => $"{x.Key}_{x.Value}")));
    }
}
