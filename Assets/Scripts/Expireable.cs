using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expireable : MonoBehaviour, IExpireable
{
    [SerializeField]
    [Range(0.1f, 30f)]
    private float _expirationTime;

    public float ExpirationTime { get => _expirationTime; set => _expirationTime = value; }

    private void Start()
    {
        StartCoroutine("Destroy");
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_expirationTime);
        OnExpire();
        Destroy(gameObject);
    }

    public void OnExpire()
    {
    }
}
