
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
                    Description = null,
                    MaxStackSize = 5,
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
            },
            {
                2,
                new Equippable
                {
                    Id = 2,
                    Description = null,
                    MaxStackSize = 1,
                    Name = "M1911",
                    Rarity = Rarity.Common,
                    Sprite = "Sprites/M1911",
                    Prefab = "Weapons/Handgun_M1911A_Steel"
                }
            },
            {
                3,
                new Consumable
                {
                    Id = 3,
                    Description = null,
                    MaxStackSize = 5,
                    Name = "First-Aid Kit",
                    Rarity = Rarity.Common,
                    Sprite = "Sprites/FirstAid",
                    Prefab = "Food/FirstAid",
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
            },
        };
    }

    public static Item Get(int id)
    {
        Items.TryGetValue(id, out Item item);
        return item;
    }
}
