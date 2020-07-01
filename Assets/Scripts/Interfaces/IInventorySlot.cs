public interface IInventorySlot
{
    Item Item { get; set; }
    int Count { get; set; }
    void Add(int slot, Item item);
    void Remove();
}
