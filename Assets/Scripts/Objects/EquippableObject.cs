using UnityEngine;

public class EquippableObject : InteractableObject
{
    public GameObject bulletOrigin;
    public GameObject bullet;
    public bool _isEquipped;

    public void Fire()
    {
        Instantiate(bullet, bulletOrigin.transform.position, transform.rotation);
    }

    private void Update()
    {
        if (_isEquipped)
        {
            transform.rotation = Player.instance.transform.rotation;
            transform.position = Player.instance.transform.position
                + (Player.instance.transform.rotation * Vector3.forward * .35f)
                + (Player.instance.transform.rotation * Vector3.down * .10f)
                + (Player.instance.transform.rotation * Vector3.right * .25f);
            if (Input.GetKeyDown(KeyCode.Mouse0)) Fire();
        }
        else
        {
            base.Update();
        }
    }
}