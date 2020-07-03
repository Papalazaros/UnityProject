using UnityEngine;

public class EquippableObject : InteractableObject
{
    public bool _isEquipped;
    private Equippable equippableItem;

    private new void Start()
    {
        transform.rotation = mainCamera.transform.rotation;
    }

    protected void UpdatePosition()
    {
        if (equippableItem == null) equippableItem = (Equippable)Item;
        transform.rotation = Quaternion.Lerp(transform.rotation, mainCamera.transform.rotation, 10 * Time.deltaTime);
        transform.position = mainCamera.transform.position + (mainCamera.transform.rotation * equippableItem.EquipOffset);
    }

    private new void Update()
    {
        if (_isEquipped)
        {
            UpdatePosition();
        }
        else
        {
            base.Update();
        }
    }
}