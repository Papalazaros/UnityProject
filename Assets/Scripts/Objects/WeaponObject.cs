using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponObject : EquippableObject
{
    public GameObject bulletOrigin;
    public GameObject shellEjectOrigin;
    public GameObject bullet;
    public GameObject casing;

    private Animator animator;
    private bool canFire = true;
    private bool isZoomed;
    private bool isReloading;

    private AudioSource audioSource;
    public AudioClip fireSound;
    public ParticleSystem particleSystem;

    public int maxBullets = 7;

    private WeaponState weaponState;

    private new void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Event triggered by the animation that indicates the gun is ready to fire
    /// </summary>
    public void ReadyToFire()
    {
        canFire = true;
    }

    public void ReloadComplete()
    {
        weaponState.CurrentAmmoCount = maxBullets;
        isReloading = false;
    }

    private void Fire()
    {
        canFire = false;
        particleSystem.Play();
        animator.SetTrigger("Fire");
        audioSource.PlayOneShot(fireSound, 0.25f);
        Instantiate(bullet, bulletOrigin.transform.position, transform.rotation);
        EjectCasing();
        weaponState.CurrentAmmoCount--;
    }

    private void Reload()
    {
        isReloading = true;
        animator.SetTrigger("Reload");
    }

    private void EjectCasing()
    {
        GameObject bulletCasing = Instantiate(casing, shellEjectOrigin.transform.position, transform.rotation);
        Rigidbody bulletCasingRigidBody = bulletCasing.GetComponent<Rigidbody>();
        bulletCasingRigidBody.AddForce((transform.rotation * Vector3.right * 0.5f) + (transform.rotation * Vector3.up * 0.5f), ForceMode.VelocityChange);
    }

    public override void AssignObjectState()
    {
        weaponState = WeaponState.Load(ObjectStateController.instance.Get(ItemInstanceId));
    }

    public override Dictionary<string, object> GetObjectState()
    {
        return weaponState?.Export();
    }

    private new void Update()
    {
        if (_isEquipped)
        {
            if (Input.GetKeyDown(KeyCode.R) && !isReloading) Reload();
            if (Input.GetKeyDown(KeyCode.Mouse0) && !isReloading && canFire && weaponState.CurrentAmmoCount > 0 && !InputManager.instance.inputDisabled) Fire();
            if (Input.GetKeyDown(KeyCode.Mouse1) && !InputManager.instance.inputDisabled)
            {
                isZoomed = !isZoomed;
            }

            if (isZoomed)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, mainCamera.transform.rotation, 10 * Time.deltaTime);
                transform.position = mainCamera.transform.position + (mainCamera.transform.rotation * ((Vector3.forward * 0.175f) + (Vector3.down * .125f)));
            }
            else
            {
                UpdatePosition();
            }
        }
        else
        {
            base.Update();
        }
    }
}
