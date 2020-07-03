using System.Collections.Generic;

public class WeaponState
{
    public int Durability;
    public int CurrentAmmoCount;

    public Dictionary<string, object> Export()
    {
        return new Dictionary<string, object>
        {
            { "Durability", Durability},
            { "CurrentAmmoCount", CurrentAmmoCount}
        };
    }

    public static WeaponState Load(Dictionary<string, object> state)
    {
        WeaponState weaponState = new WeaponState
        {
            Durability = 100
        };

        if (state == null) return weaponState;
        weaponState.Durability = (int)state["Durability"];
        weaponState.CurrentAmmoCount = (int)state["CurrentAmmoCount"];
        return weaponState;
    }
}
