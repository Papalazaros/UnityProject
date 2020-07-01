﻿using System;

public class Item
{
    public static ItemAction[] DefaultItemActions = new ItemAction[]
    {
            ItemAction.None
    };

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Rarity Rarity { get; set; }
    public string Sprite { get; set; }
    public string Prefab { get; set; }
    public int MaxStackSize { get; set; }

    public ItemAction[] Actions { get; set; }

    public virtual bool Use()
    {
        return false;
    }
}