public interface IEquippable
{
    bool IsEquipped { get; set; }
    void Equip(BaseObject item);
    void Unequip(BaseObject item);
}