using System;

public interface IInventorySlot
{
    Item Item { get; set; }
    Guid ItemInstanceId { get; set; }
    int Count { get; set; }
    void Add(int slot, Item item, Guid itemInstanceId);
    void Remove();
}
