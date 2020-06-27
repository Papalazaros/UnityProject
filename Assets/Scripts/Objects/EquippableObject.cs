using UnityEngine;

public class EquippableObject : InteractableObject
{
    public GameObject bulletOrigin;
    public GameObject shellEjectOrigin;
    public GameObject bullet;
    public GameObject casing;
    public bool _isEquipped;
    private Animator animator;
    private bool readyToFire = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
        transform.rotation = Camera.main.transform.rotation;
    }

    /// <summary>
    /// Event triggered by the animation that indicates the gun is ready to fire
    /// </summary>
    void ReadyToFire()
    {
        readyToFire = true;
    }

    public void Fire()
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
            transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, 5 * Time.deltaTime);
            transform.position = Player.instance.transform.position
                + (Player.instance.transform.rotation * Vector3.forward * .50f)
                + (Player.instance.transform.rotation * Vector3.down * .25f);
            if (Input.GetKeyDown(KeyCode.Mouse0) && readyToFire) Fire();
        }
        else
        {
            base.Update();
        }
    }
}