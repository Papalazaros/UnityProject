using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    public static AssetLoader instance;

    private Dictionary<string, Object> Assets;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Assets = new Dictionary<string, Object>();
        }
    }

    public T Get<T>(string path) where T : Object
    {
        Assets.TryGetValue(path, out Object cachedObject);

        if (cachedObject == null)
        {
            T asset = Resources.Load<T>(path);
            Assets[path] = asset;
            return asset;
        }
        else
        {
            return cachedObject as T;
        }
    }
}
