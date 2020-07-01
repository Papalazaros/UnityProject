﻿using UnityEngine;

public class WeaponObject : EquippableObject
{
    public GameObject bulletOrigin;
    public GameObject shellEjectOrigin;
    public GameObject bullet;
    public GameObject casing;

    private Animator animator;
    private bool canFire = true;
    private bool isReloading;

    private AudioSource audioSource;
    public AudioClip fireSound;
    public ParticleSystem particleSystem;

    public int bulletsRemaining;
    public int maxBullets = 7;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        base.Start();
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
        bulletsRemaining = maxBullets;
        isReloading = false;
    }

    private void Fire()
    {
        canFire = false;
        particleSystem.Play();
        animator.SetTrigger("Fire");
        audioSource.PlayOneShot(fireSound, 0.5f);
        Instantiate(bullet, bulletOrigin.transform.position, transform.rotation);
        bulletsRemaining--;
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
        bulletCasingRigidBody.AddForce((transform.rotation * Vector3.right * 0.5f) + (transform.rotation * Vector3.up * 0.5f), ForceMode.Impulse);
    }

    private void Update()
    {
        if (_isEquipped)
        {
            UpdatePosition();
            if (Input.GetKeyDown(KeyCode.R) && !isReloading) Reload();
            if (Input.GetKeyDown(KeyCode.Mouse0) && !isReloading && canFire && bulletsRemaining > 0) Fire();
        }
        else
        {
            base.Update();
        }
    }
}
