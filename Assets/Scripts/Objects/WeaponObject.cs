using UnityEngine;

public class WeaponObject : EquippableObject
{
    public GameObject bulletOrigin;
    public GameObject shellEjectOrigin;
    public GameObject bullet;
    public GameObject casing;
    private Animator animator;
    private bool readyToFire = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
    }

    /// <summary>
    /// Event triggered by the animation that indicates the gun is ready to fire
    /// </summary>
    public void ReadyToFire()
    {
        readyToFire = true;
    }

    private void Fire()
    {
        readyToFire = false;
        animator.SetTrigger("Fire");
        Instantiate(bullet, bulletOrigin.transform.position, transform.rotation);
        GameObject bulletCasing = Instantiate(casing, shellEjectOrigin.transform.position, transform.rotation);
        Rigidbody bulletCasingRigidBody = bulletCasing.GetComponent<Rigidbody>();
        bulletCasingRigidBody.AddForce((transform.rotation * Vector3.right * 0.5f) + (transform.rotation * Vector3.up * 0.5f), ForceMode.Impulse);
    }

    private void Update()
    {
        if (_isEquipped)
        {
            UpdatePosition();
            if (Input.GetKeyDown(KeyCode.Mouse0) && readyToFire) Fire();
        }
        else
        {
            base.Update();
        }
    }
}
