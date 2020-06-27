using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 0.5f)]
    private float rate;
    [SerializeField]
    [Range(0.0f, 0.5f)]
    private float maximumOffset;
    private int moveDirection = 1;
    private Vector3 initialPosition;
    private Rigidbody rigidbody;
    private EquippableObject equippableObject;

    private void Start()
    {
        initialPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
        equippableObject = GetComponent<EquippableObject>();
    }

    private void Update()
    {
        if (rigidbody == null || rigidbody.isKinematic && (equippableObject == null || !equippableObject._isEquipped))
        {
            if (transform.position.y < initialPosition.y - maximumOffset || transform.position.y > initialPosition.y + maximumOffset)
            {
                var newPosition = initialPosition;
                newPosition.y += maximumOffset * moveDirection;
                transform.position = newPosition;
                moveDirection *= -1;
            }

            transform.position += moveDirection * Vector3.up * Time.deltaTime * rate;
            transform.Rotate(new Vector3(0, 15 * Time.deltaTime, 0));
        }
    }
}
