using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EquippableItemSlotController : MonoBehaviour
{
    [SerializeField]
    private SlotType SlotType;
    [SerializeField]
    private Image Image;
    [SerializeField]
    private Text Text;

    private void Awake()
    {
        Text.text = SlotType.ToString();
    }

    private void Start()
    {
        GameEvents.instance.OnEquippedItemChanged += EquippedItemChanged;
    }

    //IEnumerator LoadAsset<T>(T t, string assetBundleName, string objectNameToLoad) where T : Object
    //{
    //    string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");
    //    filePath = System.IO.Path.Combine(filePath, assetBundleName);

    //    var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(filePath);
    //    yield return assetBundleCreateRequest;

    //    AssetBundle assetBundle = assetBundleCreateRequest.assetBundle;

    //    AssetBundleRequest asset = assetBundle.LoadAssetAsync<T>(objectNameToLoad);
    //    yield return asset;

    //    Debug.Log(asset.asset);
    //    t = asset.asset as T;
    //}

    private void EquippedItemChanged(SlotType slotType)
    {
        if (SlotType == slotType)
        {
            EquippableObject equippableObject = Player.instance.equippedItems[slotType];

            if (equippableObject == null)
            {
                Image.sprite = null;
                Image.color = new Color(255, 255, 255, 0);
            }
            else
            {
                Image.sprite = AssetLoader.instance.Get<Sprite>($"Sprites/{equippableObject.Item.Id}");
                Image.preserveAspect = true;
                Image.color = new Color(255, 255, 255, 255);
            }
        }
    }
}
