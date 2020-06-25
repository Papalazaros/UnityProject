using UnityEngine;

public sealed class Player : MonoBehaviour
{
    public static Player instance;
    public IHealth Health;

    [Range(0.1f, 0.5f)]
    public float groundDistance;
    [Range(2.5f, 5.0f)]
    public float speed;
    [Range(0f, 5f)]
    public float dashDistance;

    private CharacterController _controller;
    private GameObject _colliderBottom;
    private bool _isGrounded;
    private bool _canDash = true;
    private float _dashTime = 1.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Health = GetComponent<IHealth>();
        _controller = GetComponent<CharacterController>();
        _colliderBottom = transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        bool previouslyGrounded = _isGrounded;
        _isGrounded = Physics.Raycast(_colliderBottom.transform.position, Vector3.down, groundDistance);

        if (!_canDash) _dashTime -= Time.deltaTime;
        if (!_canDash && _dashTime <= 0) _canDash = true;
        if (_isGrounded && !previouslyGrounded) _canDash = true;

        transform.Rotate(0.0f, Input.GetAxis("Mouse X"), 0.0f, Space.Self);

        Vector3 inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");

        if (!_isGrounded) inputs *= .75f;

        Quaternion rotation = transform.rotation;
        rotation.x = 0;
        rotation.z = 0;
        Vector3 rotateVector = rotation * inputs;

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            _controller.Move(rotateVector * 50f * Time.deltaTime);

            _canDash = false;
            _dashTime = 1.0f;
        }

        _controller.Move(rotateVector * Time.deltaTime * speed);
    }
}
