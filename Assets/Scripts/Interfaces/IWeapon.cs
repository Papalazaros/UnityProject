using UnityEngine;

public interface IWeapon : IEquippable
{
    void Fire(Vector3 origin);
}
