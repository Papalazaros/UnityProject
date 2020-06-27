public interface IInventorySlot
{
    Item Item { get; set; }
    int Count { get; set; }
    void AddItem(int slot, Item item);
    void RemoveItem(int slot, Item item);
    void UseItem(int slot, Item item);
}
