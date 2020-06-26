using UnityEngine;

public class Projectile: MonoBehaviour, IProjectile
{
    public float Damage { get; set; } = 25.0f;
    public float Weight { get; set; } = 1.0f;
    public float Expiration { get; set; } = 1.0f;
    public float Speed { get; set; } = 1.0f;
    public bool UseGravity { get; set; }
    public ProjectileType ProjectileType { get; set; }

    private void Awake()
    {
        transform.Rotate(0, 90, Camera.main.transform.eulerAngles.x);
    }

    private void Update()
    {
        transform.position += transform.rotation * Quaternion.Euler(0, -90, 0) * Vector3.forward * Speed * Time.deltaTime;
        if (UseGravity) transform.position += Vector3.down * Weight * Time.deltaTime;
        Expiration -= Time.deltaTime;
        if (Expiration <= 0) Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }
}