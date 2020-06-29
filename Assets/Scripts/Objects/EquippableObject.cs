using UnityEngine;

public class EquippableObject : InteractableObject
{
    public bool _isEquipped;

    private void Start()
    {
        base.Start();
        transform.rotation = Camera.main.transform.rotation;
    }

    protected void UpdatePosition()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, 5 * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position,
            Player.instance.originPoint.transform.position + (Player.instance.transform.rotation * ((Equippable)item).EquipOffset),
            0.75f);
    }

    private void Update()
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