using UnityEngine;

public sealed class Player: MonoBehaviour
{
    public static Player instance;
    public IHealth Health;

    [Range(2.5f, 5.0f)]
    public float speed;
    [Range(0.1f, 0.5f)]
    public float groundDistance;
    [Range(0.1f, 0.5f)]
    public float ceilingDistance;
    [Range(0f, 5f)]
    public float jumpHeight;
    [Range(0f, 5f)]
    public float dashDistance;
    public Vector3 drag;

    private CharacterController _controller;
    private bool _isGrounded;
    private Vector3 _velocity;
    private GameObject _colliderBottom;
    private GameObject _colliderTop;
    private bool _canDash = true;
    private float _dashTime = 1.0f;
    private Vector3 lastPosition;

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
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _colliderTop = transform.GetChild(0).gameObject;
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
            _velocity += Vector3.Scale(rotateVector,
                dashDistance * new Vector3(
                    Mathf.Log(1f / ((Time.deltaTime * drag.x) + 1)) / -Time.deltaTime,
                    0,
                    Mathf.Log(1f / ((Time.deltaTime * drag.z) + 1)) / -Time.deltaTime)
                );

            _canDash = false;
            _dashTime = 1.0f;
        }

        _controller.Move(rotateVector * Time.deltaTime * speed);

        if (Input.GetButton("Jump") && _isGrounded) _velocity.y += Mathf.Sqrt(-2f * jumpHeight * Physics.gravity.y);
        if (_isGrounded && _velocity.y < 0) _velocity.y = 0f;
        if (!_isGrounded && _velocity.y > 0 && lastPosition.y == transform.position.y && Physics.Raycast(_colliderTop.transform.position, Vector3.up, ceilingDistance)) _velocity.y = 0;
        if (!_isGrounded && _velocity.y < 0) _velocity.y += Physics.gravity.y * 1.5f * Time.deltaTime;

        _velocity.y += Physics.gravity.y * Time.deltaTime;

        if ((inputs.x == 0 && inputs.z == 0) || (_isGrounded && !previouslyGrounded))
        {
            _velocity.z = 0;
            _velocity.x = 0;
        }
        else
        {
            _velocity.x /= 1 + (drag.x * Time.deltaTime);
            _velocity.z /= 1 + (drag.z * Time.deltaTime);
        }

        _velocity.y /= 1 + (drag.y * Time.deltaTime);

        _controller.Move(_velocity * Time.deltaTime);
        lastPosition = transform.position;
    }
}
