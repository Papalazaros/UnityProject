
using UnityEngine;

public class Equippable : Consumable
{
    public static new ItemAction[] DefaultItemActions = new ItemAction[]
    {
        ItemAction.Combine,
        ItemAction.Drop,
        ItemAction.Equip
    };

    public Vector3 EquipOffset;
    public override bool Use()
    {
        return true;
    }
}