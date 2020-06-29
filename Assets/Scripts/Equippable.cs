
using UnityEngine;

public class Equippable : Consumable
{
    public Vector3 EquipOffset;
    public override bool Use()
    {
        return true;
    }
}