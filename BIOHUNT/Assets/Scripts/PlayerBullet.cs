using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerBullet : MonoBehaviour
{
    [Header("Component")]
    public Rigidbody2D bulletRB;
    public GameObject impactEffect;

    [Header("BulletValue")]
    public float bulletSpeed = 8f;
    public int damageOfBullet = 50;

    // Update is called once per frame
    void Update()
    {
        // about bullet movement
        bulletRB.velocity = transform.right * bulletSpeed;
    }

    // about bullet hit system
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);

        // give damage only that tag the Enemy
        if(other.tag == "Enemy")
        {
            other.GetComponent<ZombieController>().DamageEnemy(damageOfBullet);
        }
    }

    // if bullet goes of of camera sight then destory gameobject
    private void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}