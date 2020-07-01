using System;

[Flags]
public enum ItemAction
{
    None = 0,
    Drop = 1,
    Use = 2,
    Equip = 4,
    Combine = 8,
    All = Drop | Use | Equip | Combine
}
