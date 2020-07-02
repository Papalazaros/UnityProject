using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField, Range(0.1f, 0.5f)]
    private float ceilingDistance;
    [SerializeField, Range(0f, 5f)]
    private float jumpHeight;
    private Vector3 velocity;
    private float jumpCooldown;
    private GameObject colliderTop;

    private void Awake()
    {
        colliderTop = transform.GetChild(0).gameObject;
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

        jumpCooldown = .75f;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && Player.instance.isGrounded) DoJump();
        if (Player.instance.isGrounded && velocity.y < 0) velocity.y = 0f;
        if (!Player.instance.isGrounded && velocity.y < 0) velocity.y += Physics.gravity.y * 1.5f * Time.deltaTime;
        if (!Player.instance.isGrounded && velocity.y > 0 && Physics.Raycast(colliderTop.transform.position, Vector3.up, ceilingDistance)) velocity.y = 0;
        if (jumpCooldown > 0) jumpCooldown -= Time.deltaTime;

        velocity.y += Physics.gravity.y * Time.deltaTime;

        Player.instance.controller.Move(velocity * Time.deltaTime);
    }
}
