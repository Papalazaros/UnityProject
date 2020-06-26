using UnityEngine;

public class EquippableObject : InteractableObject
{
    public GameObject bulletOrigin;
    public GameObject bullet;
    //private Camera mainCamera;
    public bool _isEquipped;

    public void Fire()
    {
        Instantiate(bullet, bulletOrigin.transform.position, transform.parent.rotation);
    }

    //private void Start()
    //{
    //    mainCamera = Camera.main;
    //}

    private void LateUpdate()
    {
        if (_isEquipped)
        {
            //Vector3 rotateVector = transform.parent.transform.rotation * ((Vector3.forward * .50f) + (Vector3.down * .10f) + (Vector3.right * .25f));
            //transform.position = transform.parent.transform.position + rotateVector;
            //transform.eulerAngles = new Vector3(0, transform.parent.transform.eulerAngles.y + 90, mainCamera.transform.eulerAngles.x);
            if (Input.GetKeyDown(KeyCode.Mouse0)) Fire();
        }
    }
}