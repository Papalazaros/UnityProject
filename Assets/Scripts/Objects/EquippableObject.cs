public class EquippableObject : InteractableObject, IEquippable
{
    public bool IsEquipped { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Equip(BaseObject item)
    {
        throw new System.NotImplementedException();
    }

    public void Unequip(BaseObject item)
    {
        throw new System.NotImplementedException();
    }
}
