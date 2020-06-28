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

    private void Start()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        rigidBody.AddForce(ray.direction * _speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IHealth health = collision.transform.GetComponent<IHealth>();
        Debug.Log(collision.transform.name);
        health?.TakeDamage(Damage, 0);
        Destroy(gameObject);
    }
}