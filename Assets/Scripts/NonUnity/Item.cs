public class Item
{
    public static ItemAction[] DefaultItemActions = new ItemAction[]
    {
        ItemAction.None
    };

    public SlotType SlotType;

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SpritePath { get; set; }
    public string PrefabPath { get; set; }
    public int MaxStackSize { get; set; }

    public ItemAction[] Actions { get; set; }

    public virtual bool Use()
    {
        return false;
    }
}