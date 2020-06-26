using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 0.5f)]
    private float ceilingDistance;
    [SerializeField]
    [Range(0f, 5f)]
    private float jumpHeight;
    private Vector3 velocity;
    private bool isGrounded;
    private float jumpCooldown;
    private CharacterController controller;
    private GameObject colliderTop;
    private GameObject colliderBottom;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        colliderTop = transform.GetChild(0).gameObject;
        colliderBottom = transform.GetChild(1).gameObject;
    }

    private void DoJump()
    {
        if (jumpCooldown > 0)
        {
            velocity.y += Mathf.Sqrt(-2f * jumpHeight * Physics.gravity.y * .50f);
        }
        else
        {
            velocity.y += Mathf.Sqrt(-2f * jumpHeight * Physics.gravity.y);
        }

        jumpCooldown = 1;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(colliderBottom.transform.position, Vector3.down, Player.instance.groundDistance);

        if (Input.GetButton("Jump") && isGrounded) DoJump();
        if (isGrounded && velocity.y < 0) velocity.y = 0f;
        if (!isGrounded && velocity.y < 0) velocity.y += Physics.gravity.y * 1.5f * Time.deltaTime;
        if (!isGrounded && velocity.y > 0 && Physics.Raycast(colliderTop.transform.position, Vector3.up, ceilingDistance)) velocity.y = 0;
        if (jumpCooldown > 0) jumpCooldown -= Time.deltaTime;

        velocity.y += Physics.gravity.y * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
