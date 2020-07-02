﻿using System;
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
    public EquippableObject equippedItemObject;
    public GameObject originPoint;

    public void EquipItem(Item item, Guid itemInstanceId)
    {
        if (equippedItemObject != null && equippedItemObject.ItemInstanceId == itemInstanceId)
        {
            Destroy(equippedItemObject.gameObject);
            equippedItemObject = null;
        }
        else if (equippedItemObject == null)
        {
            InstantiateObject(itemInstanceId);
        }
        else if (equippedItemObject != null && equippedItemObject.ItemInstanceId != itemInstanceId)
        {
            Destroy(equippedItemObject.gameObject);
            InstantiateObject(itemInstanceId);
        }
    }
    
    private void InstantiateObject(Guid itemInstanceId)
    {
        equippedItemObject = Instantiate(Resources.Load<EquippableObject>(item.Prefab), transform.position, transform.rotation);
        equippedItemObject.ItemInstanceId = itemInstanceId;
        equippedItemObject._isEquipped = true;
    }

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
}
