using UnityEngine;

public class ItemDetailsController : MonoBehaviour
{
    public GameObject ActionsHolder;
    public ItemActionController ItemActionController;
    public int Slot;
    public bool isInitialized;

    public void CreateActions(ItemAction[] itemActions)
    {
        foreach (ItemAction action in itemActions)
        {
            ItemActionController actionController = Instantiate(ItemActionController, ActionsHolder.transform);
            actionController.ActionText = action;
            actionController.Initialize();
        }

        isInitialized = true;
    }

    public void ResetActions()
    {
        for (int i = 0; i < ActionsHolder.transform.childCount; i++)
        {
            Destroy(ActionsHolder.transform.GetChild(i).gameObject);
        }

        isInitialized = false;
    }
}
