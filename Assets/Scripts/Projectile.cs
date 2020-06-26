using UnityEngine;

public class Projectile: MonoBehaviour, IProjectile
{
    [SerializeField]
    [Range(0.1f, 100f)]
    private float _damage;
    [SerializeField]
    [Range(0.1f, 100f)]
    private float _weight;
    [SerializeField]
    [Range(0.1f, 30f)]
    private float _expiration;
    [SerializeField]
    [Range(0.1f, 100f)]
    private float _speed;
    [SerializeField]
    private bool _useGravity;
    [SerializeField]
    private ProjectileType _projectileType;

    public float Damage { get => _damage; set => _damage = value; }
    public float Weight { get => _weight; set => _weight = value; }
    public float Expiration { get => _expiration; set => _expiration = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public bool UseGravity { get => _useGravity; set => _useGravity = value; }
    public ProjectileType ProjectileType { get => _projectileType; set => _projectileType = value; }

    private void Awake()
    {
        transform.Rotate(0, 90, Camera.main.transform.eulerAngles.x);
    }

    private void Update()
    {
        transform.position += transform.rotation * Quaternion.Euler(0, -90, 0) * Vector3.forward * _speed * Time.deltaTime;
        if (_useGravity) transform.position += Vector3.down * _weight * Time.deltaTime;
        _expiration -= Time.deltaTime;
        if (_expiration <= 0) Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }
}