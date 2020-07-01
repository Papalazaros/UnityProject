public enum ItemAction
{
    None,
    Drop,
    Use,
    Equip,
    Combine,
    All = Drop | Use | Equip | Combine
}
