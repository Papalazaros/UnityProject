using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float rate;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float maximumOffset;
    private int moveDirection = 1;
    private Vector3 initialPosition;
    private Rigidbody rigidbody;

    private void Start()
    {
        initialPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!rigidbody.isKinematic) return;

        if (transform.position.y < initialPosition.y - maximumOffset || transform.position.y > initialPosition.y + maximumOffset)
        {
            var newPosition = initialPosition;
            newPosition.y += maximumOffset * moveDirection;
            transform.position = newPosition;
            moveDirection *= -1;
        }

        transform.position += moveDirection * Vector3.up * Time.deltaTime * rate;
        transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0));
    }
}
