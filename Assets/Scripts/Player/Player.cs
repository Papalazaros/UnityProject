using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class Player : MonoBehaviour
{
    public static Player instance;
    public IHealth Health;
    [Range(0.1f, 0.5f)]
    public float groundDistance;
    [Range(2.5f, 5.0f)]
    public float baseMovementSpeed;
    public float currentMovementSpeed;
    public CharacterController controller;
    private GameObject colliderBottom;
    public bool isGrounded;
    public GameObject originPoint;
    public Dictionary<SlotType, EquippableObject> equippedItems;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Health = GetComponent<IHealth>();
        controller = GetComponent<CharacterController>();
        colliderBottom = transform.GetChild(1).gameObject;
        currentMovementSpeed = baseMovementSpeed;
        equippedItems = new Dictionary<SlotType, EquippableObject>
        {
            { SlotType.Auxiliary, null },
            { SlotType.Chest, null },
            { SlotType.Head, null },
            { SlotType.Legs, null },
            { SlotType.Weapon, null }
        };
    }

    private void Start()
    {
        GameEvents.instance.OnItemEquipped += EquipItem;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(colliderBottom.transform.position, Vector3.down, groundDistance);

        transform.Rotate(0.0f, Input.GetAxis("Mouse X"), 0.0f, Space.Self);

        Vector3 inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");

        if (!isGrounded) inputs *= .75f;

        controller.Move(transform.rotation * inputs * Time.deltaTime * currentMovementSpeed);
    }

    public void DestroyEquippedItem(Guid id)
    {
        EquippableObject equippableObject = equippedItems.FirstOrDefault(x => x.Value != null && x.Value.ItemInstanceId == id).Value;

        if (equippableObject != null)
        {
            Destroy(equippableObject.gameObject);
            equippedItems[equippableObject.Item.SlotType] = null;
            GameEvents.instance.EquippedItemChanged(equippableObject.Item.SlotType);
        }
    }

    public void EquipItem(Item item, Guid itemInstanceId)
    {
        EquippableObject equippedItemObject = equippedItems[item.SlotType];

        if (equippedItemObject != null)
        {
            if (equippedItemObject.ItemInstanceId == itemInstanceId)
            {
                Destroy(equippedItemObject.gameObject);
                equippedItems[item.SlotType] = null;
            }
            else
            {
                Destroy(equippedItemObject.gameObject);
                equippedItems[item.SlotType] = InstantiateObject(item, itemInstanceId);
            }
        }
        else
        {
            equippedItems[item.SlotType] = InstantiateObject(item, itemInstanceId);
        }

        GameEvents.instance.EquippedItemChanged(item.SlotType);
    }

    private EquippableObject InstantiateObject(Item item, Guid itemInstanceId)
    {
        EquippableObject itemObject = Instantiate(AssetLoader.instance.Get<EquippableObject>($"Prefabs/{item.Id}"), transform.position, transform.rotation);
        itemObject.ItemInstanceId = itemInstanceId;
        itemObject._isEquipped = true;
        return itemObject;
    }
}
