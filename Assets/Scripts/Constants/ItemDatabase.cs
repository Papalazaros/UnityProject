
using System.Collections.Generic;

public static class ItemDatabase
{
    private static Dictionary<int, Item> Items { get; }

    public static IReadOnlyCollection<Item> ItemsArray
    {
        get
        {
            return Items.Values;
        }
    }

    static ItemDatabase()
    {
        Items = new Dictionary<int, Item>
        {
            {
                1,
                new Consumable
                {
                    Id = 1,
                    Description = "A tin can full of Minestrone.",
                    MaxStackSize = 1,
                    Name = "Minestrone",
                    Rarity = Rarity.Common,
                    Sprite = "Sprites/1",
                    Prefab = "Food/Can_1",
                    Effects = new Effect[]
                    {
                        new Effect
                        {
                            Duration = 0,
                            EffectType = EffectType.TakeDamage,
                            Amount = -10
                        }
                    }
                }
            }
        };
    }

    public static Item Get(int id)
    {
        Items.TryGetValue(id, out Item item);
        return item;
    }
}
