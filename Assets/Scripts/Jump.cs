using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Range(0.1f, 0.5f)]
    public float ceilingDistance;
    [Range(0f, 5f)]
    public float jumpHeight;
    private Vector3 _velocity;
    private bool _isGrounded;
    private float jumpCooldown;

    public CharacterController _controller;
    public GameObject _colliderTop;
    public GameObject _colliderBottom;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _colliderTop = transform.GetChild(0).gameObject;
        _colliderBottom = transform.GetChild(1).gameObject;
    }

    private void DoJump()
    {
        if (jumpCooldown > 0)
        {
            _velocity.y += Mathf.Sqrt(-2f * jumpHeight * Physics.gravity.y * .50f);
        }
        else
        {
            _velocity.y += Mathf.Sqrt(-2f * jumpHeight * Physics.gravity.y);
        }

        jumpCooldown = 1;
    }

    private void Update()
    {
        _isGrounded = Physics.Raycast(_colliderBottom.transform.position, Vector3.down, Player.instance.groundDistance);

        if (Input.GetButton("Jump") && _isGrounded) DoJump();
        if (_isGrounded && _velocity.y < 0) _velocity.y = 0f;
        if (!_isGrounded && _velocity.y < 0) _velocity.y += Physics.gravity.y * 1.5f * Time.deltaTime;
        if (!_isGrounded && _velocity.y > 0 && Physics.Raycast(_colliderTop.transform.position, Vector3.up, ceilingDistance)) _velocity.y = 0;
        if (jumpCooldown > 0) jumpCooldown -= Time.deltaTime;

        _velocity.y += Physics.gravity.y * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }
}
