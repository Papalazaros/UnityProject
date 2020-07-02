using UnityEngine;

public class EquippableObject : InteractableObject
{
    public bool _isEquipped;

    private new void Start()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    protected void UpdatePosition()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, 5 * Time.deltaTime);
        transform.position = Player.instance.originPoint.transform.position + (Player.instance.transform.rotation * ((Equippable)Item).EquipOffset);
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