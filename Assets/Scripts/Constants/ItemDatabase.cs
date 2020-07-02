
using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    private static Dictionary<int, Item> Items { get; }

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
                    SpritePath = "Sprites/1",
                    PrefabPath = "Food/Can_1",
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
                    SpritePath = "Sprites/M1911",
                    PrefabPath = "Weapons/Handgun_M1911A_Steel",
                    EquipOffset = (Vector3.forward * .50f) + (Vector3.down * .25f),
                    Actions = Equippable.DefaultItemActions,
                    SlotType = SlotType.Weapon
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
                    SpritePath = "Sprites/FirstAid",
                    PrefabPath = "Food/FirstAid",
                    Effects = new Effect[]
                    {
                        new Effect
                        {
                            Duration = 0,
                            EffectType = EffectType.TakeDamage,
                            Amount = -10
                        }
                    },
                    Actions = Consumable.DefaultItemActions
                }
            },
            {
                4,
                new Equippable
                {
                    Id = 4,
                    Description = null,
                    MaxStackSize = 1,
                    Name = "Flashlight",
                    SpritePath = "Sprites/Flashlight",
                    PrefabPath = "Weapons/Flashlight",
                    EquipOffset = (Vector3.forward * .50f) + (Vector3.down * .25f) + (Vector3.left * .50f),
                    Actions = Equippable.DefaultItemActions,
                    SlotType = SlotType.Auxiliary
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
