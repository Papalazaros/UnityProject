using System.Collections;
using UnityEngine;

public class Projectile: MonoBehaviour, IProjectile
{
    [SerializeField]
    [Range(0.1f, 100f)]
    private float _damage;
    [SerializeField]
    [Range(0.1f, 100f)]
    private float _speed;

    public float Damage { get => _damage; set => _damage = value; }
    public float Speed { get => _speed; set => _speed = value; }

    private Vector3 newPos;
    private Vector3 oldPos;
    public float hitForce = 50f;

    private IEnumerator CheckForCollision()
    {
        newPos = transform.position;
        oldPos = newPos;

        while (true)
        {
            Vector3 velocity = transform.forward * Speed;
            newPos += velocity * Time.deltaTime;
            Vector3 direction = newPos - oldPos;
            float distance = direction.magnitude;

            if (Physics.Raycast(oldPos, direction, out RaycastHit hit, distance))
            {
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(direction * hitForce);
                    IHealth health = hit.transform.GetComponent<IHealth>();
                    health?.TakeDamage(Damage);
                    Destroy(gameObject);
                    yield break;
                }

                newPos = hit.point;
            }

            yield return new WaitForFixedUpdate();
            transform.position = newPos;
            oldPos = newPos;
        }
    }

    private void Start()
    {
        StartCoroutine(CheckForCollision());
    }
}