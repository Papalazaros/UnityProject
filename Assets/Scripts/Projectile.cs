using UnityEngine;

public class Projectile: MonoBehaviour, IProjectile
{
    [Range(0.1f, 30f)]
    public float expiration = 1.0f;
    [Range(0.1f, 100f)]
    public float damage = 25.0f;
    public float weight = 1.0f;
    [Range(0.1f, 100f)]
    public float speed = 1.0f;
    public bool useGravity;
    public ProjectileType projectileType;

    public float Damage { get => damage; set => damage = value; }
    public float Weight { get => weight; set => weight = value; }
    public float Expiration { get => expiration; set => expiration = value; }
    public float Speed { get => speed; set => speed = value; }
    public bool UseGravity { get => useGravity; set => useGravity = value; }
    public ProjectileType ProjectileType { get => projectileType; set => projectileType = value; }

    private void Awake()
    {
        transform.Rotate(0, 90, Camera.main.transform.eulerAngles.x);
    }

    private void Update()
    {
        transform.position += transform.rotation * Quaternion.Euler(0, -90, 0) * Vector3.forward * speed * Time.deltaTime;
        if (useGravity) transform.position += Vector3.down * weight * Time.deltaTime;
        expiration -= Time.deltaTime;
        if (expiration <= 0) Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }
}