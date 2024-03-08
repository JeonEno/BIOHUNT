using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 8f;
    public Rigidbody2D bulletRB;
    public GameObject impactEffect;

    // Update is called once per frame
    void Update()
    {
        bulletRB.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}

