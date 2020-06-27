using UnityEngine;

public class Projectile: MonoBehaviour, IProjectile
{
    [SerializeField]
    [Range(0.1f, 100f)]
    private float _damage;
    [SerializeField]
    [Range(0.1f, 30f)]
    private float _expiration;
    [SerializeField]
    [Range(0.1f, 100f)]
    private float _speed;

    public float Damage { get => _damage; set => _damage = value; }
    public float Expiration { get => _expiration; set => _expiration = value; }
    public float Speed { get => _speed; set => _speed = value; }

    private void Start()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(transform.rotation * Vector3.forward * _speed * 10 * Time.deltaTime, ForceMode.Impulse);
    }

    private void Update()
    {
        //transform.position += ;
        //if (_useGravity) transform.position += Vector3.down * _weight * Time.deltaTime;
        _expiration -= Time.deltaTime;
        if (_expiration <= 0) Destroy(gameObject);
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    Destroy(gameObject);
    //}
}