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

    private void Update()
    {
        isGrounded = Physics.Raycast(colliderBottom.transform.position, Vector3.down, groundDistance);

        transform.Rotate(0.0f, Input.GetAxis("Mouse X"), 0.0f, Space.Self);

        Vector3 inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");

        if (!isGrounded) inputs *= .75f;

        Quaternion rotation = transform.rotation;
        rotation.x = 0;
        rotation.z = 0;
        Vector3 rotateVector = rotation * inputs;

        controller.Move(rotateVector * Time.deltaTime * currentMovementSpeed);
    }
}
